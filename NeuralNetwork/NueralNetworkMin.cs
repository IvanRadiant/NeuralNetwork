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
            return Sigmoid(x) * (1 - Sigmoid(x));
        }
    }

    /// <summary>
    /// Нейрон
    /// </summary>
    class Neuron
    {

        double inducedLocalField;//индуцированное локально поле
        double[] weigths;//веса

        public double[] Weigths
        {
            get { return weigths; }
            set { weigths = value; }
        }
        public double Output
        {
            get { return inducedLocalField; }
        }

        public Neuron(double[] weigths)
        {
            this.weigths = weigths;
        }

        

        /// <summary>
        /// Функция возбуждения, активации, активатор
        /// </summary>
        /// <param name="input">массив входящих сигналов</param>
        public void Activate(double[] input)
        {
            inducedLocalField = 0;

            for (int i = 0; i < weigths.Length; i++)
            {
                inducedLocalField += weigths[i] * input[i];
            }

            inducedLocalField = NueralNetworkMath.Sigmoid(inducedLocalField);
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

        public double[] Input
        {
            get { return input; }
            set { input = value; }
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
            for(int i = 0; i < countOfNeuronsOfPrevLayer; i++)
            {
                weights[i] = rnd.NextDouble() + 0.001;
            }
            return weights;
        }

        protected virtual void ActivateNeurons()
        {
            if(neurons != null)
            {
                for(int i = 0; i < countNeurons; i++)
                {
                    if(neurons[i] == null)
                        neurons[i] = new Neuron(InitWeights());

                    neurons[i].Activate(input);
                }
            }
        }

        public double[] GetOutput(double[] input)
        {
            if(input != null)
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
    }

    class OutputLayer : Layer
    {
        public OutputLayer(int _countNeurons, int _countOfNeuronsOfPrevLayer, LayerType _layerType) : base(_countNeurons, _countOfNeuronsOfPrevLayer, _layerType)
        {
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

        public NeuralNetwork(int _countInputLayerNeurons,int _countHiddenLayerNeurons, int _countOutputLayerNeurons)
        {
            inputLayer = new InputLayer(_countInputLayerNeurons, 0, LayerType.Input);
            hiddenLayer = new HiddenLayer(_countHiddenLayerNeurons, _countInputLayerNeurons, LayerType.Hidden);
            outputLayer = new OutputLayer(_countOutputLayerNeurons, _countHiddenLayerNeurons, LayerType.Output);
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
                this.output[i] = new Prediction { Digital = i + 1, Probability = output[i] };
            }
        }
    }
}
