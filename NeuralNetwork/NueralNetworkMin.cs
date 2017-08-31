using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{

    enum LayerType { Input, Hidden, Output }

    /// <summary>
    /// Мат. класс
    /// </summary>
    static class NueralNetworkMath
    {
        /// <summary>
        /// sigmoid(x)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        /// <summary>
        /// sigmoid(x)dx
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double SigmoidDX(double x)
        {
            double s = Sigmoid(x);
            return s * (1 - s);
        }

        public static double RoundUp(double number, int digits)
        {
            double factor = Convert.ToDouble(Math.Pow(10, digits));
            return Math.Ceiling(number * factor) / factor;
        }
    }

    /// <summary>
    /// Нейрон
    /// </summary>
    class Neuron
    {

        double inducedLocalField;//индуцированное локально поле
        double derivativeInducedLocalField;
        double[] weights;//веса
        double error;

        public double DerivativeInducedLocalField
        {
            get { return derivativeInducedLocalField; }
        }
        public double[] Weights
        {
            get { return weights; }
            set { weights = value; }
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
            get { return error * NueralNetworkMath.SigmoidDX(inducedLocalField); }
        }

        public Neuron(double[] weights)
        {
            this.weights = weights;
        }



        /// <summary>
        /// Функция возбуждения, активации, активатор
        /// </summary>
        /// <param name="input">массив входящих сигналов</param>
        public void Activate(double[] input)
        {
            inducedLocalField = 0;

            for (int i = 0; i < weights.Length; i++)
            {
                inducedLocalField += weights[i] * input[i];
            }
            derivativeInducedLocalField = NueralNetworkMath.RoundUp(NueralNetworkMath.SigmoidDX(inducedLocalField),5);
            inducedLocalField = NueralNetworkMath.RoundUp(NueralNetworkMath.Sigmoid(inducedLocalField),5);
        }
        /// <summary>
        /// Функция возбуждения, активации, активатор, для
        /// нейронов из входного слоя(Input Layer)
        /// </summary>
        /// <param name="input">входящий сигнал</param>
        public void Activate(double input)
        {
            inducedLocalField = input;
        }

    }

    /// <summary>
    /// Слой нейросети
    /// </summary>
    abstract class Layer
    {
        protected Neuron[] neurons;
        protected int countNeurons;
        protected int countOfNeuronsOfPrevLayer;
        protected double[] input;
        protected LayerType layerType;
        private static Random rnd = new Random();
        double totalError;

        public double[] Input
        {
            get { return input; }
            set { input = value; }
        }

        public Neuron[] Neurons
        {
            get { return neurons; }
        }

        public Layer(int _countNeurons, int _countOfNeuronsOfPrevLayer, LayerType _layerType)
        {
            this.countNeurons = _countNeurons;
            this.countOfNeuronsOfPrevLayer = _countOfNeuronsOfPrevLayer;
            this.neurons = new Neuron[countNeurons];
            this.layerType = _layerType;
        }

        protected double[] InitWeights()
        {
            double[] weights = new double[countOfNeuronsOfPrevLayer];
            for (int i = 0; i < countOfNeuronsOfPrevLayer; i++)
            {
                weights[i] =  NueralNetworkMath.RoundUp(rnd.NextDouble() * (0.0009 - 0.0001) + 0.0001, 5);
            }
            return weights;
        }

        protected virtual void ActivateNeurons()
        {
            if (neurons != null)
            {
                for (int i = 0; i < countNeurons; i++)
                {
                    if (neurons[i] == null)
                        neurons[i] = new Neuron(InitWeights());

                    neurons[i].Activate(input);
                }
            }
        }

        public double[] GetOutput(double[] input)
        {
            if (input != null)
            {
                this.input = input;
                ActivateNeurons();

                if (this.layerType != LayerType.Input)
                {
                    double[] output = new double[countNeurons];
                    for (int i = 0; i < countNeurons; i++)
                        output[i] = neurons[i].Output;

                    return output;
                }

                return input;
            }

            return null;
        }

    }


    class InputLayer : Layer
    {
        public InputLayer(int _countNeurons, int _countNueronsNextLayer, LayerType _layerType) : base(_countNeurons, _countNueronsNextLayer, _layerType)
        {
        }

        protected override void ActivateNeurons()
        {
            if (neurons != null)
            {
                for (int i = 0; i < countNeurons; i++)
                {
                    neurons[i] = new Neuron(null);
                    neurons[i].Activate(input[i]);
                }
            }
        }
    }

    class HiddenLayer : Layer
    {
        public HiddenLayer(int _countNeurons, int _countOfNeuronsOfPrevLayer, LayerType _layerType) : base(_countNeurons, _countOfNeuronsOfPrevLayer, _layerType)
        {
        }

        private double GetTotalError(Neuron[] _neurons, int _indexOfRelationship)
        {
            double totalError = 0;
            for(int i = 0; i < _neurons.Length; i++)
            {
                totalError += _neurons[i].Weights[_indexOfRelationship] * _neurons[i].Error;
            }
            return totalError;
        }

        public void CalibrateWeights(Neuron[] inputNeurons, double _learningRate)
        {
            for(int i = 0; i < neurons.Length; i++)
            {
                Neuron currentNeuron = neurons[i];
                currentNeuron.Error = NueralNetworkMath.RoundUp(currentNeuron.DerivativeInducedLocalField * GetTotalError(inputNeurons, i), 5);
                for(int j = 0; j < currentNeuron.Weights.Length; j++)
                {
                    double DeltaWeights = NueralNetworkMath.RoundUp(_learningRate * currentNeuron.Error * input[j],5);
                    currentNeuron.Weights[j] = NueralNetworkMath.RoundUp(currentNeuron.Weights[j] + DeltaWeights, 5);
                    /*if (currentNeuron.Weights[i] >= 1.0)
                    {
                        currentNeuron.Weights[i] = 0.999;
                    }
                    if (currentNeuron.Weights[i] <= -1.0)
                    {
                        currentNeuron.Weights[i] = -0.999;
                    }*/
                }          
            }
        }

    }

    class OutputLayer : Layer
    {
        public OutputLayer(int _countNeurons, int _countOfNeuronsOfPrevLayer, LayerType _layerType) : base(_countNeurons, _countOfNeuronsOfPrevLayer, _layerType)
        {
        }


        public void CalibrateWeights(int _indexOfNeuron, double _error, double _learningRate)
        {
            Neuron currentNeuron = neurons[_indexOfNeuron];
            currentNeuron.Error = NueralNetworkMath.RoundUp(currentNeuron.DerivativeInducedLocalField * _error, 5);
            
            for(int i = 0; i < currentNeuron.Weights.Length; i++)
            {
                double DeltaWeights = NueralNetworkMath.RoundUp(_learningRate * currentNeuron.Error * input[i],5);
                currentNeuron.Weights[i] = NueralNetworkMath.RoundUp(currentNeuron.Weights[i] + DeltaWeights,5);
               /* if(currentNeuron.Weights[i] >= 1.0 )
                {
                    currentNeuron.Weights[i] = 0.999;
                }
                if (currentNeuron.Weights[i] <= -1.0)
                {
                    currentNeuron.Weights[i] = -0.999;
                }*/
            }
        }
       
    }

    class Prediction
    {
        public int Digital { get; set; }
        public double Probability { get; set; }

        public override string ToString()
        {
            return String.Format("Вероятность того, что на картинке цифра '{0}' = {1}", Digital, Probability);
        }
    }

    class NeuralNetwork
    {
        InputLayer inputLayer;
        HiddenLayer hiddenLayer;
        OutputLayer outputLayer;
        Prediction[] output;
        int epochs;
        double learningRate = 0.1;

        public InputLayer InputLayer
        {
            get { return inputLayer; }
        }
        public HiddenLayer HiddenLayer
        {
            get { return hiddenLayer; }
        }
        public OutputLayer OutputLayer
        {
            get { return outputLayer; }
        }
        public Prediction[] Output
        {
            get { return output; }
        }
        public int Epochs
        {
            set { epochs = value; }
        }

        public NeuralNetwork(int _countInputLayerNeurons, int _countHiddenLayerNeurons, int _countOutputLayerNeurons, int _epochs = 5000)
        {
            inputLayer = new InputLayer(_countInputLayerNeurons, 0, LayerType.Input);
            hiddenLayer = new HiddenLayer(_countHiddenLayerNeurons, _countInputLayerNeurons, LayerType.Hidden);
            outputLayer = new OutputLayer(_countOutputLayerNeurons, _countHiddenLayerNeurons, LayerType.Output);
            epochs = _epochs;
        }

        public void Train(Dictionary<int, double[]> trainSet)
        {
            for (int i = 0; i < epochs; i++)
                foreach (var train in trainSet)
                {
                    Run(train.Value);//прошли от начала до конца
                    Calibrate(train.Key);//начинаем обратный проход
                }

        }

        private void Calibrate(int expectedResult)
        {
            for(int i = 0; i < output.Length; i++)
            {
                double error = 0;

                if(output[i].Digital == expectedResult)
                {
                    error = 1 - output[i].Probability;
                }
                else
                {
                    error = 0 - output[i].Probability;
                }

                outputLayer.CalibrateWeights(i, error, learningRate);
            }
            hiddenLayer.CalibrateWeights(outputLayer.Neurons, learningRate);
        }

        public void Run(double[] input)
        {
            double[] output = outputLayer.GetOutput(
                                    hiddenLayer.GetOutput(
                                        inputLayer.GetOutput(input)
                                    )
                               );
            this.output = new Prediction[output.Length];
            for (int i = 0; i < output.Length; i++)
            {
                this.output[i] = new Prediction { Digital = i, Probability = output[i] };
            }
        }
    }
}
