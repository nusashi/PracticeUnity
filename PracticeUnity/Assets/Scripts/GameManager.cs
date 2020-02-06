using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public enum ActiveInputKey
	{
		none = KeyCode.None,
		w = KeyCode.W,
		a = KeyCode.A,
		s = KeyCode.S,
		d = KeyCode.D
	}

	public struct BaggageMatrix
	{
		public BaggageMatrix (int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		public int x { get; set; }
		public int y { get; set; }
	}
	private const int LINE_MAX_COUNT = 10;
	private const int BAGGAGE_MAX_COUNT = 1;
	[SerializeField] private Material _white = null;
	[SerializeField] private Material _black = null;
	[SerializeField] private Material _blue = null;
	[SerializeField] private Material _green = null;
	[SerializeField] private Material _olive = null;
	private List<List<GameObject>> _cubeMatrixList = null;
	private List<BaggageMatrix> _initializeSelectableMatrixDataList = null;
	private List<BaggageMatrix> _baggageMatrixDataList = null;
	private List<BaggageMatrix> _holeMatrixDataList = null;
	private BaggageMatrix _playerMatrixData;
	private ActiveInputKey _inputKey = ActiveInputKey.none;

	private void Start ()
	{
		_cubeMatrixList = new List<List<GameObject>> ();
		_initializeSelectableMatrixDataList = new List<BaggageMatrix> ();
		_baggageMatrixDataList = new List<BaggageMatrix> ();
		_holeMatrixDataList = new List<BaggageMatrix> ();
		_playerMatrixData = new BaggageMatrix (0, 0);
		CreatePlayCube ();
		InitializeWall ();
		InitializeBaggage ();
		InitializeHole ();
		InitializePlayer ();
	}

	private void Update ()
	{
		if (_inputKey == ActiveInputKey.none)
		{
			foreach (ActiveInputKey key in Enum.GetValues (typeof (ActiveInputKey)))
			{
				if (Input.GetKeyDown ((UnityEngine.KeyCode) key))
				{
					_inputKey = key;
				}
			}
		}
		else if (Input.GetKeyUp ((UnityEngine.KeyCode) _inputKey))
		{
			// TODO 移動処理
		}
	}

	private void CreatePlayCube ()
	{
		for (int x = 0; x < LINE_MAX_COUNT; x++)
		{
			_cubeMatrixList.Add (new List<GameObject> ());
			for (int y = 0; y < LINE_MAX_COUNT; y++)
			{
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				Rigidbody rigidbody = cube.AddComponent<Rigidbody> ();
				rigidbody.useGravity = false;
				cube.GetComponent<MeshRenderer> ().material = _white;
				cube.transform.position = new Vector3 (x, y, 0);
				_cubeMatrixList[x].Add (cube);
				_initializeSelectableMatrixDataList.Add (new BaggageMatrix (x, y));
			}
		}
	}

	private bool IsWallZone (int x, int y)
	{
		// x が 0行目またはMax行目であり
		// y が 0列目またはMax列目であれば壁
		return (x == 0 || x == LINE_MAX_COUNT - 1) || (y == 0 || y == LINE_MAX_COUNT - 1);
	}

	private void InitializeWall ()
	{
		for (int x = 0; x < LINE_MAX_COUNT; x++)
		{
			for (int y = 0; y < LINE_MAX_COUNT; y++)
			{
				if (IsWallZone (x, y))
				{
					_cubeMatrixList[x][y].GetComponent<MeshRenderer> ().material = _black;
					_initializeSelectableMatrixDataList.Remove (new BaggageMatrix (x, y));
				}
			}
		}
	}

	private bool IsBaggageZone (int x, int y)
	{
		return (x > 0 || x < LINE_MAX_COUNT - 1) || (y > 0 || y < LINE_MAX_COUNT - 1);
	}

	private void InitializeBaggage ()
	{
		for (int i = 0; i < BAGGAGE_MAX_COUNT; i++)
		{
			int x = UnityEngine.Random.Range (1, LINE_MAX_COUNT - 2);
			int y = UnityEngine.Random.Range (1, LINE_MAX_COUNT - 2);

		}
	}

	private void InitializeHole ()
	{

	}

	private void InitializePlayer ()
	{

	}
}