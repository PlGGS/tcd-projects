﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projects
{
    public partial class frmMazeRunner : Form
    {
        const int maxChunksInRow = 3;
        const int chunkWidth = 20;
        const int chunkHeight = 20;
        const int mazeWidth = 20;
        const int mazeHeight = 20;
        public enum Directions
        {
            Up,
            Down,
            Left,
            Right
        }
        public enum Opposites
        {
            Down,
            Up,
            Right,
            Left
        }
        Chunk[,] maze = new Chunk[mazeWidth, mazeHeight];
        Chunk currentChunk; //TODO reminder to always use public setter for this boi
        public Chunk CurrentChunk
        {
            get
            {
                return currentChunk;
            }
            set
            {
                currentChunk = value;
                currentChunk.Visited = true;
                AddNeighbors(currentChunk);
            }
        }
        Random rnd = new Random();

        public frmMazeRunner()
        {
            InitializeComponent();
            LoadChunks();
        }

        private void frmMazeRunner_Load(object sender, EventArgs e)
        {
            CreateMaze();
        }

        private void LoadChunks()
        {
            for (int y = 0; y < mazeHeight; y++)
            {
                for (int x = 0; x < mazeWidth; x++)
                {
                    maze[x, y] = new Chunk(x * chunkWidth, y * chunkHeight, chunkWidth, chunkHeight);
                }
            }
        }

        void Reset()
        {
            foreach (Chunk chunk in maze)
            {
                chunk.Reset();
            }
        }

        public void CreateMaze()
        {
            Reset();
            CurrentChunk = maze[0, 0];
            
            while (UnvisitedChunksExist())
            {
                if (ChunkHasUnvisitedNeighbors(currentChunk))
                {
                    Directions neighborDir = ChooseRandomNeighborDirection(currentChunk);
                    RemoveNeighboringWalls(currentChunk, currentChunk.Neighbors[(int)neighborDir], neighborDir, (Opposites)neighborDir);
                    CurrentChunk = currentChunk.Neighbors[(int)neighborDir];
                }
                else
                {
                    CurrentChunk = RandomUnvisitedChunk();
                }
            }
        }

        private Chunk RandomUnvisitedChunk()
        {
            Chunk chunk = maze[rnd.Next(0, mazeWidth), rnd.Next(0, mazeHeight)];

            if (chunk.Visited == true)
            {
                RandomUnvisitedChunk();
            }

            return chunk; //TODO if this is too time consuming, create a stack of all unvisited cells to choose from
        }

        private bool ChunkHasUnvisitedNeighbors(Chunk chunk)
        {
            foreach (Chunk neighbor in chunk.Neighbors)
            {
                if (neighbor != null)
                {
                    if (neighbor.Visited == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void RemoveNeighboringWalls(Chunk chunk, Chunk neighbor, Directions neighborDir, Opposites chunkDir)
        {
            chunk.Walls[(int)neighborDir] = false;

            if (neighbor != null)
            {
                neighbor.Walls[(int)chunkDir] = false;
            }
        }

        private bool UnvisitedChunksExist()
        {
            foreach (Chunk chunk in maze)
            {
                if (chunk.Visited == false)
                {
                    return true;
                }
            }

            return false;
        }

        private void AddNeighbors(Chunk chunk)
        {
            try { chunk.Neighbors[0] = maze[chunk.MazeSpot.X, chunk.MazeSpot.Y - 1]; maze[chunk.MazeSpot.X, chunk.MazeSpot.Y - 1].Print(); } catch (Exception) { }
            try { chunk.Neighbors[1] = maze[chunk.MazeSpot.X, chunk.MazeSpot.Y + 1]; maze[chunk.MazeSpot.X, chunk.MazeSpot.Y + 1].Print(); } catch (Exception) { }
            try { chunk.Neighbors[2] = maze[chunk.MazeSpot.X - 1, chunk.MazeSpot.Y]; maze[chunk.MazeSpot.X - 1, chunk.MazeSpot.Y].Print(); } catch (Exception) { }
            try { chunk.Neighbors[3] = maze[chunk.MazeSpot.X + 1, chunk.MazeSpot.Y]; maze[chunk.MazeSpot.X + 1, chunk.MazeSpot.Y].Print(); } catch (Exception) { }
        }

        Directions ChooseRandomNeighborDirection(Chunk chunk)
        {
            Directions dir = (Directions)rnd.Next(0, 3);

            if (chunk.Neighbors[(int)dir] == null)
            {
                ChooseRandomNeighborDirection(chunk);
            }
            else if (chunk.Neighbors[(int)dir].Visited == true)
            {
                ChooseRandomNeighborDirection(chunk);
            }

            return dir;
        }
    }
}
