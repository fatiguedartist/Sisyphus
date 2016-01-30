using UnityEngine;
using System.Collections.Generic;

namespace Sisyphus
{
    public class TestRoomGenerator : MonoBehaviour
    {
        public TestRoomGenerator generatorPrefab;

        private Color _endColor;
        private Color _pathColor;

        private Room _room;
        private Color _startColor;
        private Color _wireColor;
        public int depth = 15;
        public int height = 15;
        public int width = 15;
        public float scale = 1;
        public List<Material> skyboxes;

        public GameObject player;

        private void Start()
        {
            RenderSettings.skybox = skyboxes.SelectRandom();
            GameState.Instance.LevelChanged += GoToNextLevel;

            InitFields(GameState.Instance.Level);
            Solver.GenerateSolutionPath(_room);
            GenGeometry();
            transform.localScale = scale * Vector3.one;
            player.transform.position = _room.entryPoint.ToV3() * scale;
        }

        private void GoToNextLevel(int level)
        {
            Application.LoadLevel(0);
            /*
            GameState.Instance.LevelChanged -= GoToNextLevel;
            var roomGen = generatorPrefab.transform.Instantiate();
            roomGen.transform.position = Vector3.zero;
            roomGen.transform.rotation = Quaternion.identity;
            roomGen.transform.localScale = Vector3.one;
            */
            /*float angle;
            Vector3 axis;
            transform.rotation.ToAngleAxis(out angle, out axis);
            roomGen.transform.RotateAround(player.transform.position, axis, angle);*/
           // Destroy(gameObject);
        }

        private void InitFields(int level)
        {
            _wireColor = Color.red;
            _pathColor = Color.magenta;
            _startColor = Color.red;
            _endColor = Color.green;

            //_pathColor.a = 0.75f;
            _wireColor.a = 0.25f;

            _room = new Room(level, level, level, 0);
        }

        private void GenGeometry()
        {
            Vector3 baseSize;
            Vector3 basePos;

            for (var x = 0; x < _room.Width; x++)
            for (var y = 0; y < _room.Height; y++)
            for (var z = 0; z < _room.Depth; z++)
            {
                var side = _room.roomBuffer[x, y, z];
                baseSize = Vector3.one;
                basePos = new Vector3(x, y, z);

                var posIntVector = basePos.ToIntVector();
                if (posIntVector.Equals(_room.entryPoint))
                {
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = basePos;
                    geo.transform.localScale = baseSize*0.5f;
                    geo.GetComponent<MeshRenderer>().material.color = Color.red;
                    Destroy(geo.GetComponent<Collider>());
                }
                else if (posIntVector.Equals(_room.door))
                {
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = basePos;
                    geo.transform.localScale = baseSize*0.5f;
                    geo.GetComponent<MeshRenderer>().material.color = Color.green;
                    geo.AddComponent<LevelCompleteSurface>();
                }

                if ((side & Sides.Top) > 0)
                {
                    var pos = basePos + Vector3.up*0.5f;
                    var size = baseSize.ScaleBy(1, 0.1f, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Bottom) > 0)
                {
                    var pos = basePos - Vector3.up*0.5f;
                    var size = baseSize.ScaleBy(1, 0.1f, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Left) > 0)
                {
                    var pos = basePos + Vector3.left*0.5f;
                    var size = baseSize.ScaleBy(0.1f, 1, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Right) > 0)
                {
                    var pos = basePos + Vector3.right*0.5f;
                    var size = baseSize.ScaleBy(0.1f, 1, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Front) > 0)
                {
                    var pos = basePos + Vector3.forward*0.5f;
                    var size = baseSize.ScaleBy(1, 1, 0.1f);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Rear) > 0)
                {
                    var pos = basePos - Vector3.forward*0.5f;
                    var size = baseSize.ScaleBy(1, 1, 0.1f);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = size;
                }

                if (side == Sides.None)
                {
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.parent = transform;
                    geo.transform.localPosition = basePos;
                    geo.transform.localScale = baseSize;
                    geo.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    geo.AddComponent<DeadlySurface>();
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_room == null)
                return;

            Gizmos.color = Color.magenta;

            int itemIndex = -1;
            for (int index = 0; index < _room.solutionPath.Count; index++)
            {
                var int3 = _room.solutionPath[index];
                itemIndex++;

                if (itemIndex > 0)
                {
                    Gizmos.DrawLine(_room.solutionPath[itemIndex - 1].ToV3(), int3.ToV3());
                }
            }
            /*
            Vector3 size;
            Vector3 pos;
for (var x = 0; x < width; x++)
{
    for (var y = 0; y < height; y++)
    {
        for (var z = 0; z < depth; z++)
        {
            var side = _room.roomBuffer[x, y, z];
            size = Vector3.one*scale*-1;
            pos = new Vector3(x, y, z)*scale;

            var posIntVector = pos.ToIntVector();
            if (posIntVector.Equals(_room.entryPoint))
            {
                Gizmos.color = _startColor;
                Gizmos.DrawCube(pos, size);
            }
            else if (posIntVector.Equals(_room.door))
            {
                Gizmos.color = _endColor;
                Gizmos.DrawCube(pos, size);
            }
                if ((side & Sides.Top) > 0)
                {
                    var pos = pos + Vector3.up*0.5f;
                    var size = size.ScaleBy(1, 0.1f, 1);

                    Gizmos.color = _pathColor;
                    Gizmos.DrawCube(pos, size);
                }

                if ((side & Sides.Bottom) > 0)
                {
                    var pos = pos + Vector3.up * 0.5f;
                    var size = size.ScaleBy(1, 0.1f, 1);

                    Gizmos.color = _pathColor;
                    Gizmos.DrawCube(pos, size);
                }

                if ((side & Sides.Left) > 0)
                {
                    var pos = pos + Vector3.left * 0.5f;
                    var size = size.ScaleBy(0.1f, 1, 1);

                    Gizmos.color = _pathColor;
                    Gizmos.DrawCube(pos, size);
                }

                if ((side & Sides.Right) > 0)
                {
                    var pos = pos + Vector3.right * 0.5f;
                    var size = size.ScaleBy(0.1f, 1, 1);

                    Gizmos.color = _pathColor;
                    Gizmos.DrawCube(pos, size);
                }

                if ((side & Sides.Front) > 0)
                {
                    var pos = Vector3.forward * 0.5f;
                    var size = size.ScaleBy(1, 1, 0.1f);

                    Gizmos.color = _pathColor;
                    Gizmos.DrawCube(pos, size);
                }

                if ((side & Sides.Rear) > 0)
                {
                    var pos = pos + Vector3.forward * 0.5f;
                    var size = size.ScaleBy(1, 1, 0.1f);

                    Gizmos.color = _pathColor;
                    Gizmos.DrawCube(pos, size);
                }
            if (side == Sides.None)
            {
                Gizmos.color = _wireColor;
                Gizmos.DrawWireCube(pos, size);
            }
        }
        }
    }
            */
        }
    }
}