using OpenCvSharp;
using SkiaSharp;
using System;

namespace ImagePreprocess
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            string imagePath = @"C:\Users\clevi-desk-lhwn\OneDrive - Clevi\바탕 화면\Test\KakaoTalk_20240210_200707849.jpg"; // 이미지 경로 설정
            PreprocessImageWithVaryingParameters(imagePath);
        }

        public static void PreprocessImageWithVaryingParameters(string imagePath)
        {
            int[] thresholdValues = { 100, 128, 156 }; // 이진화 임계값
            int[] kernelSizes = { 3, 5 }; // Gaussian Blur 커널 사이즈
            double[] sigmaValues = { 0.5, 1.0 }; // Gaussian Blur 시그마 값
            int[] dilationSizes = { 1, 2 }; // 팽창 커널 사이즈

            foreach (var thresholdValue in thresholdValues)
            {
                foreach (var kernelSize in kernelSizes)
                {
                    foreach (var sigmaValue in sigmaValues)
                    {
                        foreach (var dilationSize in dilationSizes)
                        {
                            string resultPath = $"processed_{thresholdValue}_{kernelSize}_{sigmaValue}_dilation{dilationSize}.png";

                            using (var image = new Mat(imagePath, ImreadModes.Grayscale))
                            {
                                // 이진화
                                Cv2.Threshold(image, image, thresholdValue, 255, ThresholdTypes.Binary);

                                // Gaussian Blur 적용
                                Cv2.GaussianBlur(image, image, new OpenCvSharp.Size(kernelSize, kernelSize), sigmaValue);

                                // 팽창 적용
                                Mat dilationKernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(dilationSize, dilationSize));
                                Cv2.Dilate(image, image, dilationKernel);

                                // 결과 이미지 저장
                                image.SaveImage(resultPath);
                                Console.WriteLine($"Image processed and saved: {resultPath}");
                            }
                        }
                    }
                }
            }
        }
    }
}
