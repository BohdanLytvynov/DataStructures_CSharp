
using GraphsMath.Graphs.MultiDimGrid;
using GraphsMath.SolvingOfProblems.Interfaces;


namespace GraphsMath.SolvingOfProblems
{
    public class MultiDimGridShortestPathProblem<TGridItem> : ISolver
    {
        #region Fields
        IGrid<TGridItem, int> m_grid;

        DirectionVectors<int> m_dirVectors;

        MultiDimQueue<int> m_queue;

        Func<dynamic, List<int>, bool> m_GetValueOfVisitMatrix;

        Action<dynamic, List<int>, bool> m_SetValueOfVisitMatrix;
        #endregion

        #region Properties
        public IGrid<TGridItem, int> Grid { get => m_grid; }
        #endregion

        #region Ctor
        public MultiDimGridShortestPathProblem(IGrid<TGridItem, int> Grid,
            DirectionVectors<int> dirVectors,
            Func<dynamic, List<int>, bool> getValueOfVisitMatrix,
            Action<dynamic, List<int>, bool> setValueOfVisitMatrix)
        {
            m_grid = Grid;

            if (m_grid.Rank != dirVectors.DimensionsCount)
            {
                throw new IncorrectAmountOfDimensionsException("Incorrect amount of dimension vectors!!!");
            }

            m_dirVectors = dirVectors;

            m_queue = new MultiDimQueue<int>(m_grid.Rank);

            m_GetValueOfVisitMatrix = getValueOfVisitMatrix;

            m_SetValueOfVisitMatrix = setValueOfVisitMatrix;
        }


        #endregion

        #region Methods

        private static bool PointEqual<T>(List<T> p1, List<T> p2)
        {
            if (p1.Count != p2.Count)
            {
                return false;
            }

            for (int i = 0; i < p1.Count; i++)
            {
                if (!p1[i].Equals(p2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static List<List<int>> ReconstructPath(List<int> start, List<int> end,
            Dictionary<List<int>, List<List<int>>> prevDictionary)
        {
            List<List<int>> path = new List<List<int>>();

            path.Add(end);

            var keys = prevDictionary.Keys.ToList();

            var temp = end;

            var intTemp = keys.Count - 1;

            bool stop = false;

            while (intTemp > -1)
            {
                for (int i = intTemp; i >= 0; i--)
                {
                    var coords = prevDictionary[keys[i]];

                    for (int j = 0; j < coords.Count; j++)
                    {
                        if (PointEqual(coords[j], temp))
                        {
                            temp = keys[i];

                            path.Add(keys[i]);

                            intTemp = i - 1;

                            stop = true;

                            break;
                        }
                    }

                    if (stop)
                    {
                        break;
                    }
                }

                stop = false;
            }

            path.Reverse();

            if (PointEqual(path[0], start))
            {
                return path;
            }

            return new List<List<int>>();
        }

        private void FillPrevDictionary(List<int> point, List<int> adjPoint,
            Dictionary<List<int>, List<List<int>>> prevDictionary)
        {
            if (point.Count == 0 || adjPoint.Count == 0)
            {
                return;
            }

            if (prevDictionary.ContainsKey(point))
            {
                prevDictionary[point].Add(adjPoint);
            }
            else
            {
                List<List<int>> adjPointsList = new List<List<int>>();

                adjPointsList.Add(adjPoint);

                prevDictionary.Add(point, adjPointsList);
            }
        }

        private bool OutOfNegativeBorderss(List<int> point)
        {
            var l = point.Count;

            for (int i = 0; i < l; i++)
            {
                if (point[i] < 0)
                {
                    return true;
                }
            }

            return false;
        }

        private bool OutOfPositiveBorders(List<int> point)
        {
            var l = point.Count;

            for (int i = 0; i < l; i++)
            {
                if (point[i] >= m_grid.Bounds[i])
                {
                    return true;
                }
            }

            return false;
        }

        private void ExploreNeighbors(List<int> point, ref int cells_in_next_layer,
            dynamic VisitMatrix, TGridItem obstacle, Dictionary<List<int>,
                List<List<int>>> prevDictionary)
        {
            long l = m_dirVectors[0].Count;
            long PointDim = point.Count;
            //Analize adjecent points

            for (int i = 0; i < l; i++)
            {
                List<int> adjPoint = new List<int>();

                for (int j = 0; j < PointDim; j++)// Iterate Point Components
                {
                    adjPoint.Add(point[j] + m_dirVectors[j][i]);//Form new point
                }

                if (OutOfPositiveBorders(adjPoint))
                {
                    continue;
                }

                if (OutOfNegativeBorderss(adjPoint))
                {
                    continue;
                }

                //Skip cell that is Visited and is Obstacle

                if (m_GetValueOfVisitMatrix(VisitMatrix, adjPoint))
                {
                    continue;
                }

                if (m_grid.GetItem(adjPoint).Equals(obstacle))
                {
                    continue;
                }

                m_queue.Enqueue(adjPoint);

                m_SetValueOfVisitMatrix?.Invoke(VisitMatrix, adjPoint, true);

                FillPrevDictionary(point, adjPoint, prevDictionary);

                cells_in_next_layer += 1;
            }
        }

        /// <summary>
        /// 1 Start Point (List of coordinates) 2 (Value to stop iteration {Endpoint})
        /// 3 - Visit Matrix of type dynamic Must be bool matrix
        /// 4 - obstacle symbol
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public SolverResult Solve(List<object> args)
        {
            SolverResult r = null;

            Exception ex = null;

            int moveCount = 0;

            int cells_left_in_layer = 1;

            int cells_in_next_layer = 0;

            bool ReachedEnd = false;

            List<int> start = (List<int>)args[0];

            TGridItem stop = (TGridItem)args[1];

            dynamic VisitMatrix = args[2];

            TGridItem obstacle = (TGridItem)args[3];

            List<int> stopCoords = null;

            if (m_grid.Rank != VisitMatrix.Rank)
            {
                throw new IncorrectAmountOfDimensionsException("Dimensions of Visit Matrix are not equal to the dimensions of a grid");
            }

            Dictionary<List<int>, List<List<int>>> m_prevDictionary =
                new Dictionary<List<int>, List<List<int>>>();

            try
            {
                m_queue.Enqueue(start);

                m_SetValueOfVisitMatrix(VisitMatrix, start, true);

                //BFS Module

                while (!m_queue.IsEmpty())
                {
                    List<int> currentPoint = m_queue.Dequeue();

                    if (m_grid.GetItem(currentPoint).Equals(stop))
                    {
                        ReachedEnd = true;

                        stopCoords = currentPoint;

                        break;
                    }
                    else
                    {
                        ExploreNeighbors(currentPoint, ref cells_in_next_layer,
                            VisitMatrix, obstacle, m_prevDictionary);

                        cells_left_in_layer -= 1;

                        if (cells_left_in_layer == 0)
                        {
                            cells_left_in_layer = cells_in_next_layer;

                            cells_in_next_layer = 0;

                            moveCount++;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ex = e;
            }

            if (ReachedEnd) //We have found exit
            {
                r = new SolverResult(nameof(MultiDimGridShortestPathProblem<TGridItem>),
                    new List<object>() { moveCount, m_prevDictionary, stopCoords }, ex == null ? false : true, ex);
            }
            else
            {
                r = new SolverResult(nameof(MultiDimGridShortestPathProblem<TGridItem>),
                    new List<object>() { -1 }, ex == null ? false : true, ex);
            }

            return r;
        }
        #endregion
    }
}