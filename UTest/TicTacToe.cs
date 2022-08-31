using NUnit.Framework;
using System;
using UTest.TicTacToeFolder;

namespace UTest
{
    [TestFixture]
    public class TicTacToe
    {
        [Test]
        public void CreateGame_GameIsIncorrectState()
        {
            Game game = new Game();
            Assert.AreEqual(0, game.MovesCounter);
            Assert.AreEqual(State.Unset, game.GetState(1));
        }

        [Test]
        public void MakeMove_CounterShifts()
        {
            Game game = new Game();
            game.MakeMove(1);

            Assert.AreEqual(1, game.MovesCounter);
        }

        [Test]
        public void MakeInvalidMove_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var game = new Game();
                game.MakeMove(0);
            });
        }

        [Test]
        public void MovesOnTheSameSqueare_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var game = new Game();
                game.MakeMove(1);
                game.MakeMove(1);
            });
        }

        [Test]
        public void MakingMoves_SetStateCorrectly()
        {
            Game game = new Game();

            // this way we refactor 4 lines of code.
            MakeMoves(game, 1, 2, 3, 4);

            Assert.AreEqual(State.Cross, game.GetState(1));
            Assert.AreEqual(State.Zero, game.GetState(2));
            Assert.AreEqual(State.Cross, game.GetState(3));
            Assert.AreEqual(State.Zero, game.GetState(4));
        }

        [Test]
        public void GetWinner_ZeroesWinVertically_ReturnsZeroes()
        {
            Game game = new Game();

            // 2, 5, 8
            MakeMoves(game, 1,2,3,5,7,8);

            Assert.AreEqual(Winner.Zeroes, game.GetWinner());
        }

        [Test]
        public void GetWinner_CrossWinDiagonal_ReturnsCrosses()
        {
            Game game = new Game();

            // 1, 5, 9 - Cross win
            MakeMoves(game, 1, 4, 5, 2, 9);

            Assert.AreEqual(Winner.Crosses, game.GetWinner());
        }

        [Test]
        public void GetWinner_GameIsUnfinished_ReturnsGameIsUnfinished()
        {
            Game game = new Game();

            MakeMoves(game, 1, 2, 4);

            Assert.AreEqual(Winner.GameIsUnfinished, game.GetWinner());
        }

        void MakeMoves(Game game, params int[] indexes)
        {
            foreach (var index in indexes)
            {
                game.MakeMove(index);
            }
        }
    }
}
