using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_102.AsWorldBuilder.Creator.Acts
{
    internal class NoiseMapper
    {
        /// <summary>
        /// 用于记录震动
        /// </summary>
        private struct Octave
        {
            public float amp;
            public float freq;

            /// <param name="amplitude">振幅</param>
            /// <param name="frequency">频率</param>
            public Octave(float amplitude, float frequency)
            {
                amp = amplitude;
                freq = frequency;
            }
        }

        public static float[,] GenerateNoiseMap(int width, int height, System.Random CreatorRandom)
        {
            Octave oct1 = new Octave(1f, 10f);
            Octave oct2 = new Octave(oct1.amp / 2, oct1.freq * 2);
            Octave oct3 = new Octave(oct2.amp / 2, oct2.freq * 2);

            float maxAmp = oct1.amp + oct2.amp + oct3.amp;//max振幅
            float absolutePeriod = 100f;//绝对周期
            float xStretch = 2.5f; //x延伸
            float zStretch = 1.6f;//z延伸

            Vector2f offset = new Vector2f((float)CreatorRandom.NextDouble(), (float)CreatorRandom.NextDouble());// x y

            float[,] noiseMap = new float[width, height];//生成储存各个格子的数据二位列表

            float total = 0f;

            for (int i = 0; i < width; i++) // 给每个格子生成一个随机数
            {
                for (int j = 0; j < height; j++) //遍历每一个格子
                {
                    Vector2f pos = new Vector2f(i / absolutePeriod + offset.x, j / absolutePeriod + offset.y);      // 查找噪声函数的当前x、y位置 -- 每一个位置会映射一个固定的值
                    double e =                                                                                      // 在[0，maxAmp]中生成一个平均maxAmp/2的值  PerlinNoise：一个噪声函数基本上是一个种子随机发生器。 它需要一个整数作为参数，然后根据这个参数返回一个随机数。 如果你两次都传同一个参数进来，它就会产生两次相同的数。
                        oct1.amp * Mathf.PerlinNoise(oct1.freq * pos.x / xStretch, oct1.freq * pos.y) +
                        oct2.amp * Mathf.PerlinNoise(oct2.freq * pos.x / xStretch, oct2.freq * pos.y) +
                        oct3.amp * Mathf.PerlinNoise(oct3.freq * pos.x / xStretch, oct3.freq * pos.y);

                    e /= maxAmp;                                                                                 // 变成一个0-1之间的数字
                    float f = Mathf.Clamp((float)e, 0f, 1f);
                    total += f;
                    //把数据放到表里
                    noiseMap[i, j] = f;
                }
            }

            // 将分布集中在0.5处，并拉伸以填充[0，1]
            float average = total / noiseMap.Length;

            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    float f = noiseMap[i, j];
                    f -= average;
                    f *= zStretch;// 获取随机数的距离平均数的距离并提高
                    f += 0.5f;// 把中心换成0.5
                    noiseMap[i, j] = Mathf.Clamp(f, 0f, 1f);
                }

            return noiseMap;
        }
    }
}
