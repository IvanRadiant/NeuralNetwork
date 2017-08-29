using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
namespace NeuralNetwork
{
    enum LayerType { Hidden, Output, Input }

    class Neurol
    {
        double inducedLocalField;
        double error;

        public Neurol()
        {
            inducedLocalField = 0;
        }

        public double Output
        {
            get { return inducedLocalField; }
        }

        public double Error
        {
            get { return error; }
            set { error = value; }
        }

        public double WeightsDelta
        {
            get { return error * (Sigmoid(inducedLocalField) * (1 - Sigmoid(inducedLocalField))); }
        }



        public void Activate(double[] weights, double[] input)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                inducedLocalField += weights[i] * input[i];
            }

            inducedLocalField = Sigmoid(inducedLocalField);
        }

        public void Activate(double input)
        {
            inducedLocalField = input;
        }

        private double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

    }

    abstract class Layer
    {
        protected int countOfNeuronsOfLayer;
        protected int countOfNeuronsOfPrevLayer;
        protected LayerType layerType;
        protected double[,] weights;
        protected double[] input;
        protected Neurol[] neurols;
        protected static Random random = new Random();
        public Layer prevLayer { get; set; }
        public Layer nextLayer { get; set; }

        public double[] Input
        {
            get { return input; }
            set { input = value; }
        }
        public int CountOfNeuronsOfLayer
        {
            get { return countOfNeuronsOfLayer; }
        }
        public int CountOfNeuronsOfPrevLayer
        {
            get { return countOfNeuronsOfPrevLayer; }
        }
        public Neurol[] Neurols
        {
            get { return neurols; }
        }

        public double[,] Weights
        {
            get { return weights; }
        }

        public Layer(int _countOfNeuronsOfLayer, int _countOfNeuronsOfPrevLayer, LayerType _layerType,  Layer _prevLayer, Layer _nextLayer)
        {
            this.countOfNeuronsOfLayer = _countOfNeuronsOfLayer;
            this.countOfNeuronsOfPrevLayer = _countOfNeuronsOfPrevLayer;
            this.layerType = _layerType;
            this.weights = new double[countOfNeuronsOfLayer, countOfNeuronsOfPrevLayer];
        
            this.neurols = new Neurol[countOfNeuronsOfLayer];
            this.prevLayer = _prevLayer;
            this.nextLayer = _nextLayer;

            InitWeights();
            InitNeurons();
        }


        protected virtual void InitNeurons()
        {
            for (int i = 0; i < neurols.Length; i++)
            {
                neurols[i] = new Neurol();
            }
        }

        protected virtual void InitWeights()
        {
            for (int i = 0; i < countOfNeuronsOfLayer; i++)
            {
                for (int j = 0; j < countOfNeuronsOfPrevLayer; j++)
                    weights[i, j] = random.NextDouble();
            }
        }

        protected virtual void activateNeurols()
        {
            for (int i = 0; i < neurols.Length; i++)
            {
                double[] output = new double[countOfNeuronsOfPrevLayer];
                for (int j = 0; j < countOfNeuronsOfPrevLayer; j++)
                {
                    output[j] = weights[i, j];
                }
                neurols[i].Activate(output, input);
            }
        }

        public double[] GetOutput()
        {
            activateNeurols();
            double[] output = new double[neurols.Length];
            for (int i = 0; i < output.Length; i++)
                output[i] = neurols[i].Output;

            return output;
        }

    }

    class InputLayer : Layer
    {
        public InputLayer(int _countOfNeuronsOfLayer, int _countOfNeuronsOfPrevLayer, LayerType _layerType,  Layer _prevLayer, Layer _nextLayer)
            : base(_countOfNeuronsOfLayer, _countOfNeuronsOfPrevLayer, _layerType,  _prevLayer, _nextLayer)
        {
        }

        protected override void InitWeights() { }

        protected override void activateNeurols()
        {
            for (int i = 0; i < neurols.Length; i++)
            {
                neurols[i].Activate(input[i]);
            }
        }
    }

    class SimpleLayer : Layer
    {
        public SimpleLayer(int _countOfNeuronsOfLayer, int _countOfNeuronsOfPrevLayer, LayerType _layerType, Layer _prevLayer, Layer _nextLayer)
            : base(_countOfNeuronsOfLayer, _countOfNeuronsOfPrevLayer, _layerType, _prevLayer, _nextLayer)
        {
        }

        public void CorrectWeights(double learningRate)
        {

            for (int i = 0; i < nextLayer.CountOfNeuronsOfLayer; i++)
            {
                for (int j = 0; j < nextLayer.CountOfNeuronsOfPrevLayer; j++)
                {
                    neurols[j].Error += nextLayer.Weights[i, j] * nextLayer.Neurols[i].WeightsDelta;
                }
            }

            for (int i = 0; i < countOfNeuronsOfLayer; i++)
            {
                for (int j = 0; j < countOfNeuronsOfPrevLayer; j++)
                {
                    double currentWeight = weights[i, j];
                    weights[i, j] = currentWeight - input[j] * neurols[i].WeightsDelta * learningRate;
                }
            }


            SimpleLayer targetLayer = prevLayer as SimpleLayer;
            if (targetLayer != null)
                targetLayer.CorrectWeights(learningRate);
        }
    }

    class OutputLayer : Layer
    {
        public OutputLayer(int _countOfNeuronsOfLayer, int _countOfNeuronsOfPrevLayer, LayerType _layerType, Layer _prevLayer, Layer _nextLayer)
            : base(_countOfNeuronsOfLayer, _countOfNeuronsOfPrevLayer, _layerType,_prevLayer, _nextLayer)
        {
        }

        public void CorrectWeights(double learningRate, int indexOfExpected)
        {


            for (int i = 0; i < countOfNeuronsOfLayer; i++)
            {

                if (i == indexOfExpected)
                {
                    neurols[i].Error = neurols[i].Output - 1;
                }
                else
                {
                    neurols[i].Error = neurols[i].Output - 0;
                }

                for (int j = 0; j < countOfNeuronsOfPrevLayer; j++)
                {
                    double currentWeight = weights[i, j];
                    weights[i, j] = currentWeight - input[j] * neurols[i].WeightsDelta * learningRate;
                }
            }

            SimpleLayer targetLayer = prevLayer as SimpleLayer;
            if (targetLayer != null)
                targetLayer.CorrectWeights(learningRate);
        }

    }

    struct Digital
    {
        public double prediction;
        public int digital;

        public override string ToString()
        {
            return String.Format("Вероятность того, что это цифра '{0}' = {1}", digital, prediction);
        }
    }

    class Network
    {
        InputLayer inputLayer;
        SimpleLayer hiddenLayer;
        OutputLayer outputLayer;
        Digital[] output;
        int epoches;
        double learningRate = 0.05;

        public Digital[] Output
        {
            get
            {
                double[] predictions = outputLayer.GetOutput();
                output = new Digital[predictions.Length];
                for (int i = 0; i < predictions.Length; i++)
                {
                    output[i].prediction = predictions[i];
                    output[i].digital = i;
                }
                return output;
            }
        }

        public Network(int _inputLayerSize, int _hiddenLayerSize, int _outPutLayerSize, int _epoches = 5000)
        {
            inputLayer = new InputLayer(_inputLayerSize, 1, LayerType.Input, null, hiddenLayer);
            hiddenLayer = new SimpleLayer(_hiddenLayerSize, _inputLayerSize, LayerType.Hidden, inputLayer, outputLayer);
            outputLayer = new OutputLayer(_outPutLayerSize, _hiddenLayerSize, LayerType.Output, hiddenLayer, null);

            inputLayer.nextLayer = hiddenLayer;
            hiddenLayer.nextLayer = outputLayer;

            epoches = _epoches;
        }

        public void Run(double[] input)
        {
            inputLayer.Input = input;
            hiddenLayer.Input = inputLayer.GetOutput();
            outputLayer.Input = hiddenLayer.GetOutput();
        }

        public void Prediction(int expectedFor, double[] input)
        {

            for (int j = 0; j < 100; j++)
            {
                for (int i = 0; i < Output.Length; i++)
                {
                    if (Output[i].digital == expectedFor)
                    {
                        outputLayer.CorrectWeights(learningRate, i);
                    }
                }
                Run(input);
            }
        }

        public void Train(Dictionary<int, double[]> _trainSet)
        {

        }

    }
}
*/