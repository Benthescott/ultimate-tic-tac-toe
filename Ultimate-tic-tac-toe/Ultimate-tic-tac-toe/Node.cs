﻿using System;
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

        // Value of the node
        public short Value;

        // Depth of node in tree
        public short Depth { get; set; }

        // If main board was changed
        public bool MainChanged { get; set; }

        // The board, 0-8, played on
        public short BoardNumberPlayedOn { get; set; }

        // The board, 0-8, to play on next
        public short BoardNumberToPlayOn { get; set; }

        // The player that made the move
        public bool Player { get; set; }

        public Node()
        {
            this.MainChanged = false;
            this.Depth = 0;
        }


        /// <summary>
        ///     
        ///     Copy contructor for the Node class
        /// 
        /// </summary>
        /// <param name="n"></param>
        public Node(Node n)
        {
            this.Row = n.Row;
            this.Col = n.Col;
            this.Depth = n.Depth;
            this.BoardNumberPlayedOn = n.BoardNumberPlayedOn;
            this.BoardNumberToPlayOn = n.BoardNumberToPlayOn;
            this.Player = n.Player;
            this.MainChanged = n.MainChanged;
            this.Value = n.Value;
        }


        /// <summary>
        ///     
        ///     Utility contructor
        /// 
        /// </summary>
        /// <param name="depth">
        /// 
        ///     Depth of the node
        /// 
        /// </param>
        /// <param name="BNTPO">
        /// 
        ///     Board number to play on
        /// 
        /// </param>
        /// <param name="player">
        /// 
        /// 
        ///     Max or Min player, true or false
        /// 
        /// </param>
        public Node(short depth, short BNTPO, bool player)
        {
            this.Depth = depth;
            this.BoardNumberToPlayOn = BNTPO;
            this.Player = player;
            this.MainChanged = false;
        }


        /// <summary>
        /// 
        ///     Utility contructor
        /// 
        /// </summary>
        /// <param name="BNPO">
        /// 
        ///     Board Number Played On
        /// 
        /// </param>
        /// <param name="BNTPO">
        /// 
        ///     Board number to play on
        /// 
        /// </param>
        /// <param name="row">
        /// 
        ///     3x3 Row number
        /// 
        /// </param>
        /// <param name="col">
        /// 
        ///     3x3 Column number
        /// 
        /// </param>
        public Node(short BNPO, short BNTPO, short row, short col)
        {
            this.BoardNumberPlayedOn = BNPO;
            this.BoardNumberToPlayOn = BNTPO;
            this.Row = row;
            this.Col = col;
        }
    }
}
