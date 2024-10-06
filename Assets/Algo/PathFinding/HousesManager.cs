using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
public class HousesManager : MonoBehaviour
{
    [SerializeField] private GameObject _prefabOfHouse;
    [SerializeField] private Transform _parentOfHouse;

    [SerializeField] private int _gridSizeX = 20;
    [SerializeField] private int _gridSizeY = 20;



    House[,] _allHousesArr;


    [SerializeField] private Button _initAllHousesBtn;
    [SerializeField] private Button _startGeneratingMazeBtn;
    [SerializeField] private Button _startPathFindingAlgoBtn;

    [SerializeField] private LineRenderer _lineRenderer;

    private void Awake()
    {
        Camera.main.transform.position = new Vector3(_gridSizeX / 2, 10, _gridSizeY / 2);
        _allHousesArr = new House[_gridSizeX, _gridSizeY];
        _initAllHousesBtn.onClick.AddListener(() => SpawnAt(UIntVector2.Zero));
        _startGeneratingMazeBtn.onClick.AddListener(() =>
        {
            StartCoroutine(GenerateMaze(null, _allHousesArr[0, 0]));
            _allHousesArr[0, 0].ClearDown();
            _allHousesArr[_gridSizeX - 1, _gridSizeY - 1].ClearUp();
        });

        _startPathFindingAlgoBtn.onClick.AddListener(() =>
        {
            _allHousesArr[0, 0].SetVisitColor();
            _allHousesArr[_gridSizeX - 1, _gridSizeY - 1].UpdateColor(Color.black);
            StartCoroutine(eStartPathFinding(_allHousesArr[0, 0], _allHousesArr[_gridSizeX - 1, _gridSizeY - 1]));
        });
    }

    #region Path Find Algo
    IEnumerator eStartPathFinding(House __start, House __end)
    {

        Queue<House> __queue = new();
        Dictionary<House, House> __neighbors = new();
        HashSet<House> __visitor = new();

        __queue.Enqueue(__start);
        __visitor.Add(__start);
        while (__queue.Count > 0)
        {
            House __current = __queue.Dequeue();
            if (__current == __end)
            {
                StartCoroutine(eStartShow(__neighbors, __current));
                Debug.Log($"Ended: {__neighbors.ContainsKey(__current)}");
                yield break;
            }


            foreach (House neighbor in GetNeighbors(__current))
            {
                if (__visitor.Contains(neighbor))
                    continue;

                __visitor.Add(neighbor);
                __queue.Enqueue(neighbor);
                __neighbors[neighbor] = __current;

                neighbor.SetVisitColor();
                yield return null;
            }
        }
    }

    List<House> GetNeighbors(House __house)
    {
        List<House> __houses = new();
        if (__house == null)
            return __houses;

        if (__house.left != null && !__house.left.IsAvlRightWall && !__house.IsAvlLeftWall) __houses.Add(__house.left);
        if (__house.right != null && !__house.right.IsAvlLeftWall && !__house.IsAvlRightWall) __houses.Add(__house.right);
        if (__house.up != null && !__house.up.IsAvlDownWall && !__house.IsAvlUpWall) __houses.Add(__house.up);
        if (__house.down != null && !__house.down.IsAvlUpWall && !__house.IsAvlDownWall) __houses.Add(__house.down);
        return __houses;
    }
    IEnumerator eStartShow(Dictionary<House, House> neighbors, House __currentHouse)
    {
        // int index = 0;
        List<House> reverseHouse = new();
        while (__currentHouse != null)
        {
            if (neighbors.ContainsKey(__currentHouse))
            {
                reverseHouse.Add(__currentHouse);
                __currentHouse = neighbors[__currentHouse];
                // __currentHouse.UpdateColor(Color.green);
                yield return null;
            }
            else __currentHouse = null;
        }
        reverseHouse.Reverse();

        foreach (var item in reverseHouse)
        {
            // Debug.Log($"REVERSE: {item.name}");
            item.UpdateColor(Color.green);
            yield return null;
        }

    }

    #endregion












    #region GenerateMaze

    IEnumerator GenerateMaze(House _prev, House _current)
    {
        _current.Visit();
        yield return ClearWall(_prev, _current);
        House _nextHouse;


        do
        {
            _nextHouse = GetNextUnvisitedHouses(_current);
            if (_nextHouse != null)
            {
                yield return GenerateMaze(_current, _nextHouse);
            }
        }
        while (_nextHouse != null);


        yield return null;
    }

    House GetNextUnvisitedHouses(House _current)
    {
        return GetUnvisitedHouseList(_current).OrderBy(_ => Random.Range(0, 10)).FirstOrDefault();
    }
    IEnumerable<House> GetUnvisitedHouseList(House __house)
    {
        int x = __house.position.x;
        int y = __house.position.y;


        if (x + 1 < _gridSizeX)
        {
            House _newHouse = _allHousesArr[x + 1, y];
            if (!_newHouse.IsVisited)
                yield return _newHouse;
        }

        if (x - 1 >= 0)
        {
            House _newHouse = _allHousesArr[x - 1, y];
            if (!_newHouse.IsVisited)
                yield return _newHouse;
        }


        if (y + 1 < _gridSizeY)
        {
            House _newHouse = _allHousesArr[x, y + 1];
            if (!_newHouse.IsVisited)
                yield return _newHouse;
        }

        if (y - 1 >= 0)
        {
            House _newHouse = _allHousesArr[x, y - 1];
            if (!_newHouse.IsVisited)
                yield return _newHouse;
        }
    }

    IEnumerator ClearWall(House _prev, House _current)
    {
        if (_prev == null)
            yield break;

        if (_prev.position.x < _current.position.x)
        {
            _prev.ClearRight();
            _current.ClearLeft();
            yield return null;
        }
        if (_prev.position.x > _current.position.x)
        {
            _prev.ClearLeft();
            _current.ClearRight();
            yield return null;
        }


        if (_prev.position.y < _current.position.y)
        {
            _prev.ClearUp();
            _current.ClearDown();
            yield return null;
        }
        if (_prev.position.y > _current.position.y)
        {
            _prev.ClearDown();
            _current.ClearUp();
            yield return null;
        }
    }

    #endregion




    #region Generate Houses
    IEnumerator eInitAllHouses(House __house)
    {

        if (__house == null)
            yield break;


        int x = __house.position.x;
        int y = __house.position.y;
        if (__house.left == null)
        {
            __house.left = SpawnAt(new UIntVector2(x - 1, y));
            yield return null;
        }


        if (__house.right == null)
        {
            __house.right = SpawnAt(new UIntVector2(x + 1, y));
            yield return null;
        }

        if (__house.up == null)
        {
            __house.up = SpawnAt(new UIntVector2(x, y + 1));
            yield return null;
        }

        if (__house.down == null)
        {
            __house.down = SpawnAt(new UIntVector2(x, y - 1));
            yield return null;
        }

        yield return null;

    }

    private House SpawnAt(UIntVector2 uIntVector2)
    {
        if (!IsValidLocation(uIntVector2))
            return null;

        House __house = _allHousesArr[uIntVector2.x, uIntVector2.y];

        if (__house != null)
            return __house;


        __house = Instantiate(_prefabOfHouse,
       new Vector3(uIntVector2.x, 0, uIntVector2.y),
        Quaternion.identity, _parentOfHouse)
        .GetComponent<House>();

        __house.name = $"{_parentOfHouse.childCount} [{uIntVector2.x}-{uIntVector2.y}]";

        __house.position = uIntVector2;
        _allHousesArr[uIntVector2.x, uIntVector2.y] = __house;
        StartCoroutine(eInitAllHouses((__house)));
        return __house;
    }

    private bool IsValidLocation(UIntVector2 uIntVector2)
    {
        if (uIntVector2.x < 0)
            return false;

        if (uIntVector2.y < 0)
            return false;

        if (uIntVector2.x >= _gridSizeX)
            return false;

        if (uIntVector2.y >= _gridSizeY)
            return false;

        return true;
    }
    #endregion
}