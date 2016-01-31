using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public float scale = 1;
        public List<Material> skyboxes;

        public GameObject player;

        public Entry entry;
        public Exit exit;

        public List<GameObject> leftSideWalls; 
        public List<GameObject> rightSideWalls;
        public List<GameObject> topSideWalls;
        public List<GameObject> bottomSideWalls;
        public List<GameObject> frontSideWalls;
        public List<GameObject> backSideWalls;
        public List<GameObject> centerPieces;
        public List<GameObject> acidPieces;

        private void Start()
        {
            var skybox = skyboxes.SelectRandom();
            RenderSettings.skybox = skybox;

            InitFields(GameState.Instance.Level);
            Solver.GenerateSolutionPath(_room);
            GenGeometry();
            transform.localScale = scale * Vector3.one;
            player.transform.position = _room.entryPoint.ToV3() * scale;
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
            Vector3 pos;

            for (var x = 0; x < _room.Width; x++)
            for (var y = 0; y < _room.Height; y++)
            for (var z = 0; z < _room.Depth; z++)
            {
                var side = _room.roomBuffer[x, y, z];
                baseSize = Vector3.one;
                pos = new Vector3(x, y, z);

                var posIntVector = pos.ToIntVector();
                if (posIntVector.Equals(_room.entryPoint))
                {
                    var entryNode = entry.Instantiate();
                    entryNode.transform.parent = transform;
                    entryNode.transform.localPosition = new Vector3 (pos.x,pos.y-.15f,pos.z);
                    Destroy(entryNode.GetComponent<Collider>());
                        }
                        else if (posIntVector.Equals(_room.door))
                        {
                            var exitNode = exit.InstantiateToParentLocal(transform);
                            exitNode.transform.localPosition = pos;

                            if ((side & Sides.Top) > 0)
                            {
                                exitNode.transform.localEulerAngles = Vector3.right*180;
                            }
                            if ((side & Sides.Left) > 0)
                            {
                                exitNode.transform.localEulerAngles = Vector3.forward*-90;
                            }
                            if ((side & Sides.Right) > 0)
                            {
                                exitNode.transform.localEulerAngles = Vector3.forward*90;
                            }
                            if ((side & Sides.Front) > 0)
                            {
                                exitNode.transform.localEulerAngles = Vector3.right*-90;
                            }
                            if ((side & Sides.Rear) > 0)
                            {
                                exitNode.transform.localEulerAngles = Vector3.right * 90;
                            }
                        }

                        if (x == 0)
                        {
                            if (side == Sides.None)
                    {
                        var geo = rightSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                        geo.transform.localPosition = pos + Vector3.left;
                    }
                }
                if (x == _room.Width - 1)
                {
                     if (side == Sides.None)
                    {
                        var geo = leftSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                        geo.transform.localPosition = pos + Vector3.right;
                    }
                }

                if (y == 0)
                {
                    if (side == Sides.None)
                    {
                        var geo = topSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                        geo.transform.localPosition = pos + Vector3.down;
                    }
                }
                if (y == _room.Height - 1)
                {
                    if (side == Sides.None)
                    {
                        var geo = bottomSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                        geo.transform.localPosition = pos + Vector3.up;
                    }
                }

                if (z == 0)
                {
                    if (side == Sides.None)
                    {
                        var geo = frontSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                        geo.transform.localPosition = pos + Vector3.back;
                    }
                }
                if (z == _room.Depth - 1)
                {
                    if (side == Sides.None)
                    {
                        var geo = backSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                        geo.transform.localPosition = pos + Vector3.forward;
                    }
                }

                if ((side & Sides.Top) > 0)
                {
                    var geo = topSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                    geo.transform.localPosition = pos;

                    {
                        var next = y < _room.Height - 1 ? _room.roomBuffer[x, y + 1, z] : side;
                        if (next != Sides.None && (side & Sides.Bottom) == 0)
                        {
                            var rearGeo = bottomSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                            rearGeo.transform.localPosition = pos + Vector3.up;
                        }
                    }
                        }

                if ((side & Sides.Bottom) > 0)
                {
                    var geo = bottomSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                    geo.transform.localPosition = pos;

                    {
                        var next = y > 0 ? _room.roomBuffer[x, y - 1, z] : side;
                        if (next != Sides.None && (side & Sides.Top) == 0)
                        {
                            var rearGeo = topSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                            rearGeo.transform.localPosition = pos + Vector3.down;
                        }
                    }
                }

                if ((side & Sides.Left) > 0)
                {
                    var geo = leftSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                    geo.transform.localPosition = pos;

                    {
                        var next = x > 0 ? _room.roomBuffer[x - 1, y, z] : side;
                        if (next != Sides.None && (side & Sides.Right) == 0)
                        {
                            var rearGeo = rightSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                            rearGeo.transform.localPosition = pos + Vector3.left;
                        }
                    }
                }

                if ((side & Sides.Right) > 0)
                {
                    var geo = rightSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                    geo.transform.localPosition = pos;

                    {
                        var next = x < _room.Width - 1 ? _room.roomBuffer[x + 1, y, z] : side;
                        if (next != Sides.None && (side & Sides.Left) == 0)
                        {
                            var rearGeo = leftSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                            rearGeo.transform.localPosition = pos + Vector3.right;
                        }
                    }
                }

                if ((side & Sides.Front) > 0)
                {
                    var geo = frontSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                    geo.transform.localPosition = pos;

                    {
                        var next = z < _room.Depth - 1 ? _room.roomBuffer[x, y, z + 1] : side;
                        if (next != Sides.None && (side & Sides.Rear) == 0)
                        {
                            var rearGeo = backSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                            rearGeo.transform.localPosition = pos + Vector3.forward;
                        }
                    }
                }

                if ((side & Sides.Rear) > 0)
                {
                   var geo = backSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                    geo.transform.localPosition = pos;

                    {
                        var next = z > 0 ? _room.roomBuffer[x, y, z - 1] : side;
                        if (next != Sides.None && (side & Sides.Front) == 0)
                        {
                            var rearGeo = frontSideWalls.SelectRandom().InstantiateToParentLocal(transform);
                            rearGeo.transform.localPosition = pos + Vector3.back;
                        }
                    }
                }

                if (side == Sides.None)
                {
                    var geo = acidPieces.SelectRandom().Instantiate();
                    geo.transform.parent = transform;
                    geo.transform.localPosition = pos;
                    geo.transform.localScale = baseSize;
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
                    Gizmos.DrawLine(_room.solutionPath[itemIndex - 1].ToV3() * scale, int3.ToV3() * scale);
                }
            }

            Gizmos.color = Color.blue;

            for (var divergence = 0; divergence < _room.divergentPaths.Count; divergence++)
            {
                var divergentPath = _room.divergentPaths[divergence];
                itemIndex = -1;
                for (int index = 0; index < divergentPath.Count; index++)
                {
                    var int3 = divergentPath[index];
                    itemIndex++;

                    if (itemIndex > 0)
                    {
                        Gizmos.DrawLine(divergentPath[itemIndex - 1].ToV3() * scale, int3.ToV3() * scale);
                    }
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