using System;
using System.Collections.Generic;

namespace Memento
{
    //Надає послідовний доступ до елементів об’єкта-агрегата без висвітлення його
    //внутрішньої структури.
    class Program
    {
        static void Main(string[] args)
        {
            var caretaker = new Caretaker();
            caretaker.F5();
            caretaker.ShootThatDumbAss();
            caretaker.F5();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.ShootThatDumbAss();
            caretaker.F9();
            caretaker.ShootThatDumbAss();
            caretaker.F9();
            caretaker.ShootThatDumbAss();

            Console.ReadLine();
        }
    }

    public class GameOriginator
    {
        // Стан містить здоров’я та к-ть вбитих монстрів
        private GameState _state = new GameState(100, 0);
        public void Play()
        {
            // Імітуємо процес гри –
            // здоров’я повільно погіршується, а монстрів стає все менше
            Console.WriteLine("Health {0} , Killed: {1}",_state.Health, _state.KilledMonsters);
            _state = new GameState((int)(_state.Health * 0.9), _state.KilledMonsters + 2);
        }
        public GameMemento GameSave()
        {
            return new GameMemento(_state);
        }
        public void LoadGame(GameMemento memento)
        {
            _state = memento.GetState();
        }
    }

    public class GameMemento
    {
        private readonly GameState _state;
        public GameMemento(GameState state)
        {
            _state = state;
        }
        public GameState GetState()
        {
            return _state;
        }
    }

    public class GameState
    {
        public GameState(int h, int k)
        {
            Health = h;
            KilledMonsters = k;
        }
        public int Health { get; set; }

        public int KilledMonsters { get; set; }
    }

    public class Caretaker
    {
        private readonly GameOriginator _game = new GameOriginator();
        private readonly Stack<GameMemento> _quickSaves = new Stack<GameMemento>();
        public void ShootThatDumbAss()
        {
            _game.Play();
        }
        public void F5()
        {
            _quickSaves.Push(_game.GameSave());
        }
        public void F9()
        {
            _game.LoadGame(_quickSaves.Peek());
        }
    }

}
