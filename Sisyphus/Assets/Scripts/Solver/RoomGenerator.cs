using UnityEngine;

namespace Sisyphus
{
    public class RoomGenerator : MonoBehaviour
    {
        public int width;
        public int height;
        public int depth;

        private Room _room;

        void Start()
        {
            _room = new Room(width, height, depth);
            Solver.GenerateSolutionPath(_room);
        }
    }
}
