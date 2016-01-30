using UnityEngine;

namespace Sisyphus
{
    public class RoomGenerator : MonoBehaviour
    {
        private Color _endColor;
        private Color _pathColor;

        private Room _room;
        private Color _startColor;
        private Color _wireColor;
        public int depth = 15;
        public int height = 15;
        public int width = 15;
        public float scale = 1;

        private void Start()
        {
            InitFields();
            Solver.GenerateSolutionPath(_room);
            GenGeometry();
        }

        private void InitFields()
        {
            _wireColor = Color.red;
            _pathColor = Color.magenta;
            _startColor = Color.red;
            _endColor = Color.green;

            //_pathColor.a = 0.75f;
            _wireColor.a = 0.25f;

            _room = new Room(width, height, depth, 0);
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
                baseSize = Vector3.one*scale;
                basePos = new Vector3(x, y, z) *scale;

                var posIntVector = basePos.ToIntVector();
                if (posIntVector.Equals(_room.entryPoint))
                {
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = basePos;
                    geo.transform.localScale = baseSize*0.5f;
                    geo.GetComponent<MeshRenderer>().material.color = Color.red;
                }
                else if (posIntVector.Equals(_room.door))
                {
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = basePos;
                    geo.transform.localScale = baseSize*0.5f;
                    geo.GetComponent<MeshRenderer>().material.color = Color.green;
                }

                if ((side & Sides.Top) > 0)
                {
                    var pos = basePos + Vector3.up*0.5f;
                    var size = baseSize.ScaleBy(1, 0.1f, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Bottom) > 0)
                {
                    var pos = basePos - Vector3.up*0.5f;
                    var size = baseSize.ScaleBy(1, 0.1f, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Left) > 0)
                {
                    var pos = basePos + Vector3.left*0.5f;
                    var size = baseSize.ScaleBy(0.1f, 1, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Right) > 0)
                {
                    var pos = basePos + Vector3.right*0.5f;
                    var size = baseSize.ScaleBy(0.1f, 1, 1);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Front) > 0)
                {
                    var pos = basePos + Vector3.forward*0.5f;
                    var size = baseSize.ScaleBy(1, 1, 0.1f);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = pos;
                    geo.transform.localScale = size;
                }

                if ((side & Sides.Rear) > 0)
                {
                    var pos = basePos - Vector3.forward*0.5f;
                    var size = baseSize.ScaleBy(1, 1, 0.1f);
                    var geo = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    geo.transform.position = pos;
                    geo.transform.localScale = size;
                }

                if (side == Sides.None)
                {
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_room == null)
                return;

            Vector3 size;
            Vector3 pos;

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
                        /*
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
                            */
                        if (side == Sides.None)
                        {
                            Gizmos.color = _wireColor;
                            Gizmos.DrawWireCube(pos, size);
                        }
                    }
                }
            }
        }
    }
}