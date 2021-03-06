﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Efectos
{
    class Delay : ISampleProvider
    {
        private ISampleProvider fuente;

        int offstTiempoMS;  
            

        public Delay(ISampleProvider fuente)
        {
            this.fuente = fuente;
            offstTiempoMS = 1000;
        }

        public WaveFormat WaveFormat
        {
            get
            {
                return fuente.WaveFormat;
            }
        }
        //Offset es el numero de muestras leidas hasta ahorita
        public int Read(float[] buffer, int offset, int count)
        {
            var read = fuente.Read(buffer, offset, count);
            float tiempoTranscurrido =
               (float) offset / (float)fuente.WaveFormat.SampleRate;
            float tiempoTranscurridoMS = tiempoTranscurrido * 1000;
            int numMuestrasOffsetTiempo =(int)
                (((float) offstTiempoMS / 1000.0f) * (float)fuente.WaveFormat.SampleRate);

            if (tiempoTranscurridoMS > offstTiempoMS)    
            {
                for (int i = 0; i < read; i++)
                {
                    buffer[offset + i] +=
                        buffer[offset + i - numMuestrasOffsetTiempo];
                   
                }

            }

            return read;
        }
    }
}
