using System;

namespace Proxy
{
    //Призначення. Надає замінника або утримувача для іншого об’єкту, щоб керувати ним
    class Program
    {
        static void Main(string[] args)
        {
            int opNum = 0;
            try
            {
                var proxy = new RobotBombDefuserProxy(41);
                proxy.WalkStraightForward(100);
                opNum++;
                proxy.TurnRight();
                opNum++;
                proxy.WalkStraightForward(5);
                opNum++;
                proxy.DefuseBomb();
                opNum++;
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception has been caught with message: ({0}). Decided to have human operate robot there.", e.Message);
               
                //PlanB(opNum);
            }
        }
    }

    class RobotBombDefuser
    {
        private Random _random = new Random();
        private int _robotConfiguredWavelength = 41;
        private bool _isConnected = false;
        public void ConnectWireless(int communicationWaveLength)
        {
            if (communicationWaveLength == _robotConfiguredWavelength)
            {
                _isConnected = IsConnectedImmitatingConnectivitiyIssues();
            }
        }
        public bool IsConnected()
        {
            _isConnected = IsConnectedImmitatingConnectivitiyIssues();
            return _isConnected;
        }
        private bool IsConnectedImmitatingConnectivitiyIssues()
        {
            // Імітуємо погане з’єднання (працює в 4 із 10 спробах)
            return _random.Next(0, 10) < 4;
        }
        public virtual void WalkStraightForward(int steps)
        {
            Console.WriteLine("Did {0} steps forward...", steps);
        }
        public virtual void TurnRight()
        {
            Console.WriteLine("Turned right...");
        }
        public virtual void TurnLeft()
        {
            Console.WriteLine("Turned left...");
        }
        public virtual void DefuseBomb()
        {
            Console.WriteLine("Cut red or green or blue wire...");
        }
    }

    class RobotBombDefuserProxy : RobotBombDefuser
    {
        private RobotBombDefuser _robotBombDefuser;
        private int _communicationWaveLength;
        private int _connectionAttempts = 3;
        public RobotBombDefuserProxy(int communicationWaveLength)
        {
            _robotBombDefuser = new RobotBombDefuser();
            _communicationWaveLength = communicationWaveLength;
        }
        public virtual void WalkStraightForward(int steps)
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.WalkStraightForward(steps);
        }
        public virtual void TurnRight()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.TurnRight();
        }
        public virtual void TurnLeft()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.TurnLeft();
        }
        public virtual void DefuseBomb()
        {
            EnsureConnectedWithRobot();
            _robotBombDefuser.DefuseBomb();
        }
        private void EnsureConnectedWithRobot()
        {
            if (_robotBombDefuser == null)
            {
                _robotBombDefuser = new RobotBombDefuser();
                _robotBombDefuser.ConnectWireless(_communicationWaveLength);
            }
            for (int i = 0; i < _connectionAttempts; i++)
            {
                if (_robotBombDefuser.IsConnected() != true)
                {
                    _robotBombDefuser.ConnectWireless(_communicationWaveLength);
                }
                else
                {
                    break;
                }
            }
            if (_robotBombDefuser.IsConnected() != true)
            {
                throw new Exception("No connection with remote bomb diffuser robot could be made after few attempts.");
            }
        }
    }
}
