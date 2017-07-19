using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    public class GNode<T>
    {
        public T data;
        public LinkedList<GNode<T>> adjacent = new LinkedList<GNode<T>>();
        public GNode(T data)
        {
            this.data = data;
        }
        public void addAdjacent(GNode<T> adjacentNode)
        {
            adjacent.AddLast(adjacentNode);
        }
    }

    public class Graph<T>
    {
        private Dictionary<T, GNode<T>> nodeLookup = new Dictionary<T, GNode<T>>();
        private Dictionary<T, T[]> adjcentLookup = new Dictionary<T, T[]>();

        private GNode<T> getNode(T data)
        {
            if (nodeLookup.ContainsKey(data))
            {
                return nodeLookup[data];
            }
            return null;
        }

        private GNode<T> getOrAddNode(T data)
        {
            GNode<T> result = null;
            if (nodeLookup.ContainsKey(data))
            {
                result = nodeLookup[data];
            }
            else
            {
                result = addNode(data);
            }
            return result;
        }

        public GNode<T> addNode(T data)
        {
            GNode<T> node = new GNode<T>(data);
            nodeLookup.Add(data, node);
            return node;
        }

        public GNode<T> addNode(T data, T[] adjcent)
        {
            GNode<T> node = new GNode<T>(data);
            nodeLookup.Add(data, node);
            adjcentLookup.Add(data, adjcent);
            return node;
        }

        public void addEdge(T source, T destination)
        {
            addEdge(source, new T[] { destination });
        }

        public void addEdge(T source, T[] destinations)
        {
            GNode<T> s = getOrAddNode(source);
            if (s == null) return;
            foreach (T item in destinations)
            {
                GNode<T> d = getOrAddNode(item);
                if (s.adjacent.Find(d) == null)
                    s.addAdjacent(d);
            }
        }

        public void BuildEdges()
        {
            foreach (var edges in adjcentLookup)
            {
                addEdge(edges.Key, edges.Value);
            }
        }

        public bool hasPathDFS(T source, T destination)
        {
            GNode<T> s = getNode(source);
            GNode<T> d = getNode(destination);
            HashSet<GNode<T>> visited = new HashSet<GNode<T>>();
            return hasPathDFS(s, d, visited);
        }

        private bool hasPathDFS(GNode<T> source, GNode<T> destination, HashSet<GNode<T>> visited)
        {
            if (visited.Contains(source)) return false;
            visited.Add(source);
            if (source == destination) return true;
            foreach (GNode<T> child in source.adjacent)
            {
                if (hasPathDFS(child, destination, visited))
                {
                    return true;
                }
            }
            return false;
        }

        public bool hasPathBFS(T source, T destination)
        {
            Dictionary<GNode<T>, GNode<T>> parents = new Dictionary<GNode<T>, GNode<T>>();
            return hasPathBFS(getNode(source), getNode(destination),parents);
        }

        public bool hasPathBFS(GNode<T> source, GNode<T> destination, Dictionary<GNode<T>, GNode<T>> parents)
        {
            bool hasPath = false;
            List<GNode<T>> nextToVisit = new List<GNode<T>>();
            HashSet<GNode<T>> visited = new HashSet<GNode<T>>();
            nextToVisit.Add(source);
            parents.Add(source, null);
            while (nextToVisit.Count != 0)
            {
                GNode<T> node = nextToVisit.First();
                nextToVisit.RemoveAt(0);
                if (node == destination)
                {
                    hasPath = true;
                    break;
                }
                if (visited.Contains(node)) continue;
                visited.Add(node);
                foreach (GNode<T> child in node.adjacent)
                {
                    nextToVisit.Add(child);
                    parents.Add(child, node);
                }
            }
//building the path from source node to destination node
            if (hasPath)
            {
                List<GNode<T>> path = new List<GNode<T>>();
                GNode<T> temp = destination;
                while (temp != null)
                {
                    path.Add(temp);
                    if (parents.ContainsKey(temp))
                        temp = parents[temp];
                    else temp = null;
                }
            }
            return hasPath;
        }

        public Stack<GNode<T>> getTopologicalSort()
        {
            Stack<GNode<T>> topologicalSort = new Stack<GNode<T>>();
            HashSet<GNode<T>> visited = new HashSet<GNode<T>>();
            foreach (var node in nodeLookup)
            {
                if (visited.Contains(node.Value)) continue;
                traversTopologicalSort(node.Value, topologicalSort, visited);
            }
            return topologicalSort;
        }

        private void traversTopologicalSort(GNode<T> node, Stack<GNode<T>> topologicalSort, HashSet<GNode<T>> visited)
        {
            visited.Add(node);
            foreach (GNode<T> child in node.adjacent)
            {
                if (visited.Contains(child)) continue;
                traversTopologicalSort(child, topologicalSort, visited);
            }
            topologicalSort.Push(node);
        }

        public T getLowerCommonAncestor(T root, T source, T destination)
        {
            T lca = default(T);
            Stack<GNode<T>> path1 = getPath(getNode(root), getNode(source));
            Stack<GNode<T>> path2 = getPath(getNode(root), getNode(destination));
            while (path1.Peek() == path2.Peek())
            {
                lca = path1.Pop().data;
                path2.Pop();
            }

            return lca;
        }

        private Stack<GNode<T>> getPath(GNode<T> root, GNode<T> destination)
        {
            Stack<GNode<T>> path = new Stack<GNode<T>>();
            if (root != destination)
                getPath(root, destination, path);
            path.Push(root);
            return path;
        }

        private GNode<T> getPath(GNode<T> source, GNode<T> destination, Stack<GNode<T>> path)
        {
            if (source == null) return null;
            if (source == destination) return source;
            foreach (GNode<T> child in source.adjacent)
            {
                GNode<T> node = getPath(child, destination, path);
                if (node != null)
                {
                    path.Push(child);
                    return child;
                }
            }
            return null;
        }
// the same as hasPathBFS
        private bool findRoute(GNode<T> start, GNode<T> destination)
        {
            if (start == null || destination == null) return false;
            //          if (start == destination) return true;
            Dictionary<GNode<T>, GNode<T>> parents = new Dictionary<GNode<T>, GNode<T>>();
            Queue<GNode<T>> nextToVisit = new Queue<GNode<T>>();
            HashSet<GNode<T>> visited = new HashSet<GNode<T>>();
            nextToVisit.Enqueue(start);
            parents.Add(start, start);
            bool found = false;
            while (nextToVisit.Count > 0 && !found)
            {
                GNode<T> node = nextToVisit.Dequeue();
                if (visited.Contains(node)) continue;
                if (node == destination)
                {
                    found = true;
                    break;
                }
                foreach (GNode<T> child in node.adjacent)
                {
                    parents.Add(node, child);
                    visited.Add(node);
                    nextToVisit.Enqueue(child);
                }
            }
          return found;
        }
    }
}
