using UnityEngine;

namespace Sisyphus
{
    public class RoomGenerator : MonoBehaviour
    {
        public int width = 15;
        public int height = 15;
        public int depth = 15;
        public float scale = 1;

        private Room _room;
        private Color _solidColor;
        private Color _wireColor;

        void Start()
        {
            InitFields();
            Solver.GenerateSolutionPath(_room);
        }

        private void InitFields()
        {
            _wireColor = Color.red;
            _solidColor = Color.blue;
            _solidColor.a = 0.75f;
            _room = new Room(width, height, depth, 0);
        }

        void OnDrawGizmos()
        {
            if (_room == null)
                return;

            var size = Vector3.one * scale;
            Vector3 pos;

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    for (var z = 0; z < depth; z++)
                    {
                        pos = new Vector3(x, y, z) * scale;

                        if (!_room.roomBuffer[x,y,z])
                        {
                            Gizmos.color = _solidColor;
                            Gizmos.DrawCube(pos, size);
                        }
                        else
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
