
namespace DTOs
{
    public class BoardObject
    {


        PositionObject[,] locations;
        (int, int, int) boardSize;
        int height;
        int width;


        //TODO: possible helper: in on boarder for tile(x, y)

        public void BoardObject((int, int, int) size)
        {
            boardSize = size;

            bool completed = makeBoardToSize(size);
            if (!completed)
            {
                Debug.LogError("Board not able to be made");
            }
        }

        private async bool makeBoardToSize((int, int, int) size)
        {
            // size defined in order of (bottom left edge, bottom edge, bottom right edge)
            if (size.Item1 <= 0 || size.Item2 <= 0 || size.Item3 <= 0) return false;

            height = size.Item1 + size.Item3 - 1;
            width = size.Item2 + size.Item3 - 1;

            locations = new PositionObject[height, width]

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    locations[i, j] = new PositionObject();
                }
            }

            return true;
        }

        public PositionObject getPositionObject(int x, int y)
        {

            if (!validTile(x, y)) return null;
            // passes all checks

            return locations[x, y + (boardSize.Item3 - 1)]

        }

        private bool validTile(int x, int y)
        {
            if (x < 0 || x >= width || y <= (-1 * boardSize.Item3) || y >= boardSize.Item1)
            {
                Debug.LogError("Board asked for position outside array");
                return false;
            }
            if (x >= boardSize.Item2 && y != 0)
            {

                int maxY = (width - x) - 1;
                if (y < 0)
                {
                    if (y > maxY)
                    {
                        Debug.LogError("Board asked for position in unused space");
                        return false;
                    }
                }
                else
                {
                    if (y < 0 - maxY)
                    {
                        Debug.LogError("Board asked for position in unused space");
                        return false;
                    }
                }
            }
            return true;
        }


        // TODO: get tile by clicked location
        // TODO: get tile by direction from another tile
        public (int, int) getNeighborCordinates(int x, int y, int direction)
        {
            // 1, 2, 3 are bottom to top on right side
            if (direction > 3 || direction == 0 || direction < -3) return null;
            if (!validTile(x, y)) return null;
            (int, int) result;
            switch direction{
                case -3
                    result = (-1, 1);
                    break;
                case -2
                    result = (-1, 0);
                    break;
                case -1
                    result = (-1, -1);
                    break;
                case 1
                    result = (0), 1);
                    break;
                case 2
                    result = (1, 0);
                    break;
                case 3
                    result = (0, -1);
                    break;
            }
            if (direction != 2 && direction != -2 && y != 0)
            {
                bool tempBool;
                if (y < 0)
                {
                    tempBool = ((direction + 3) % 4) == 0;
                }
                else
                {
                    tempBool = ((direction + 1) % 4) == 0;
                }
                if(tempBool){
                    result = (result.Item1 + 1, result.Item2)
                }
            }

            return result;
        }


        // TODO: get all surounding tiles



        // TODO: ?? handle line of sight calculations for board and from point to point

    }
}