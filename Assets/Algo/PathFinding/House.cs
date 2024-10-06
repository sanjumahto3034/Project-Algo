using System.Collections;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Wall Reference")]
    [SerializeField] private GameObject _leftWall;
    [SerializeField] private GameObject _rightWall;
    [SerializeField] private GameObject _upWall;
    [SerializeField] private GameObject _downWall;


    [SerializeField] private GameObject _selfWall;

    public bool IsAvlLeftWall => _leftWall.activeSelf;
    public bool IsAvlRightWall => _rightWall.activeSelf;
    public bool IsAvlUpWall => _upWall.activeSelf;
    public bool IsAvlDownWall => _downWall.activeSelf;


    public bool IsVisited { get; private set; }
    public void Visit()
    {
        _selfWall.SetActive(false);
        IsVisited = true;
    }
    public void ClearLeft() => _leftWall.SetActive(false);
    public void ClearRight() => _rightWall.SetActive(false);
    public void ClearUp() => _upWall.SetActive(false);
    public void ClearDown() => _downWall.SetActive(false);


    [Header("Houses Reference")]
    public House left;
    public House right;
    public House up;
    public House down;

    private UIntVector2 _position;
    public UIntVector2 position
    {
        get => _position;
        set
        {
            transform.position = new Vector3(value.x, 0, value.y);
            _position = value;
        }
    }



    public void SetVisitColor()
    {
        _selfWall.SetActive(true);
        _selfWall.transform.localScale = Vector3.one * 0.5f;
        _selfWall.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, 0.1f);
    }
    public void UpdateColor(Color __color)
    {
        _selfWall.SetActive(true);
        _selfWall.transform.localScale = Vector3.one * 0.5f;
        _selfWall.GetComponent<MeshRenderer>().material.color = __color;
    }
    /*
        private void OnMouseOver()
        {
            UpdateColor(Color.red);
            left?.UpdateColor(Color.red);
            right?.UpdateColor(Color.red);
            up?.UpdateColor(Color.red);
            down?.UpdateColor(Color.red);

        }
        private void OnMouseExit()
        {
            UpdateColor(Color.white);
            left?.UpdateColor(Color.white);
            right?.UpdateColor(Color.white);
            up?.UpdateColor(Color.white);
            down?.UpdateColor(Color.white);

        }
        */
}