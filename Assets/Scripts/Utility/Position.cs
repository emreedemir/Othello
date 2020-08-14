using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Othello
{

    public struct Position
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Concat(X, Y);

        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position))
            {
                return false;
            }

            var position = (Position)obj;

            return X == position.X && Y == position.Y;
        }

        public static implicit operator Vector2(Position position) => new Vector2(position.X, position.Y);

        public static Position operator -(Position position1, Position position2) => new Position(position1.X - position2.X, position1.Y - position2.Y);

        public static Position operator +(Position position1, Position position2) => new Position(position1.X + position2.X, position1.Y + position2.Y);

        public static Position operator *(Position position, int value) => new Position(position.X * value, position.Y * value);

        public static bool operator ==(Position position1, Position position2)
        {
            if (ReferenceEquals(position1, position2)) return true;
            if (ReferenceEquals(position1, null)) return false;

            return position1.Equals(position2);

        }

        public static bool operator !=(Position position1, Position position2)
        {
            if (ReferenceEquals(position1, position2)) return false;
            if (ReferenceEquals(position1, null)) return true;

            return !position1.Equals(position2);
        }

    }
}
