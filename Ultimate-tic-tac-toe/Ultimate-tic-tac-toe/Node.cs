using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Node
    {
        // X,Y Coord of last move played
        public short Row { get; set; }
        public short Col { get; set; }

        // Depth of node in tree
        public short Depth { get; set; }

        // If main board was changed
        public bool MainChanged = false;

        // The board, 0-8, played on
        public short BoardNumberPlayedOn { get; set; }

        // The board, 0-8, to play on next
        public short BoardNumberToPlayOn { get; set; }

        // The player that made the move
        public bool Player { get; set; }

        public Node()
        {
        }

        public Node(Node n)
        {
            this.Row = n.Row;
            this.Col = n.Col;
            this.Depth = n.Depth;
            this.BoardNumberPlayedOn = n.BoardNumberPlayedOn;
            this.BoardNumberToPlayOn = n.BoardNumberToPlayOn;
            this.Player = n.Player;
        }

        public Node(short depth, short BNTPO, bool player)
        {
            this.Depth = depth;
            this.BoardNumberToPlayOn = BNTPO;
            this.Player = player;
        }

    }
}
