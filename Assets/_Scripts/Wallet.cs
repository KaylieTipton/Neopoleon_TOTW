using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    public class Wallet : MonoBehaviour
    {
        public static Wallet instance;
        // Events
        public UnityEvent<double, double> coinUpdatedEvent; // delta, current coins
        public UnityEvent<double> earnEvent;
        public UnityEvent purchaseEvent;
        public UnityEvent outOfCashEvent;

        // Variables
        private double _coin = 0;

        public double Coin { get { return _coin; } }


        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (coinUpdatedEvent == null)
                coinUpdatedEvent = new UnityEvent<double, double>();

            if (earnEvent == null)
                earnEvent = new UnityEvent<double>();

            if (purchaseEvent == null)
                purchaseEvent = new UnityEvent();

            if (outOfCashEvent == null)
                outOfCashEvent = new UnityEvent();
        }

        public bool ICanAfford(double _amount)
        {
            if (0 > (_coin - _amount))
            {
                return false;
            }

            return true;
        }

        public bool Pay(double _amount)
        {
            bool iCanAfford = ICanAfford(_amount);

            if (!iCanAfford)
                return false;

            _coin -= _amount;
            if (_coin <= 0)
                outOfCashEvent.Invoke();

            purchaseEvent.Invoke();

            coinUpdatedEvent.Invoke(-_amount, _coin);

            return true;
        }

        public void Earn(double _amount, Vector3? _location = null)
        {
            if (_coin < 0)
                return;

            _coin += _amount;

            earnEvent.Invoke(_amount);
            coinUpdatedEvent.Invoke(_amount, _coin);
        }

        public void ResetCoin()
        {
            _coin = 0;
        }
    }
