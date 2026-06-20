// ============================================================
// Práctica 1: Animación con Lerp en C#
// Tools Development — Pixar Animation Studios (educativo)
// ============================================================
using System;
using System.Threading;
class AnimacionLerp
{
// Constantes de configuración de la animación
const int ANCHO_CONSOLA = 80; // Columnas de la consola
const int ALTO_CONSOLA = 25; // Filas de la consola
const int FPS = 24; // Fotogramas por segundo
const int DURACION_SEG = 3; // Duración total en segundos

const string PERSONAJE = "★"; // Carácter que representa al personaje

/// <summary>
/// Interpolación lineal entre dos valores.
/// </summary>
/// <param name="a">Valor inicial (t=0)</param>
/// <param name="b">Valor final (t=1)</param>
/// <param name="t">Progreso de 0.0 a 1.0</param>
/// <returns>Valor interpolado</returns>
static float Lerp(float a, float b, float t)
{
// Asegurar que t esté en rango [0,1]
t = Math.Clamp(t, 0f, 1f);
return a + t * (b - a);
}


/// <summary>
/// Calcula la posición horizontal del personaje.
/// </summary>
static int CalcularPosicionX(float t)
{
float xInicio = 0f;
float xFinal = ANCHO_CONSOLA - 2f; // -2 por el ancho del char
return (int)Lerp(xInicio, xFinal, t);
}
/// <summary>
/// Calcula la posición vertical del personaje.
/// </summary>
static int CalcularPosicionY(float t)
{
float yInicio = 0f;
float yFinal = ALTO_CONSOLA - 2f;
// Movimiento diagonal opcional:
return (int)Lerp(yInicio, yFinal, t);
// Para movimiento horizontal puro,
// devuelve ALTO_CONSOLA / 2 siempre.
}

static int CalcularPosicionYCurva(float t)
{
float amplitud = 4f;
float centro = ALTO_CONSOLA / 2f;
return (int)(centro + MathF.Sin(t * MathF.PI * 2f) * amplitud);
}

static void Main(string[] args)
    {
        Console.CursorVisible = false;
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Title = "Animación Lerp 24 FPS — Práctica 1";
// Calcular el intervalo de tiempo entre frames en milisegundos
int intervaloMs = 1000 / FPS; // 1000ms / 24fps ≈ 41ms por frame

// Tiempo total de la animación en milisegundos
int tiempoTotalMs = DURACION_SEG * 1000;
// Registrar el momento de inicio
DateTime tiempoInicio = DateTime.Now;
// ─── BUCLE PRINCIPAL DE ANIMACIÓN
while (true)
{
// Calcular tiempo transcurrido desde el inicio
double transcurridoMs = (DateTime.Now -
tiempoInicio).TotalMilliseconds;
// Calcular t: progreso normalizado de 0.0 a 1.0
float t = (float)(transcurridoMs / tiempoTotalMs);
// Si t supera 1.0, la animación terminó
if (t >= 1.0f) break;
// Calcular posición actual usando nuestras funciones
int posX = CalcularPosicionX(t);
int posY = CalcularPosicionY(t);
// ─── RENDERIZADO─────────────────────────────────────
Console.Clear();
// Dibujar información de debug en la primera línea
Console.SetCursorPosition(0, 0);
Console.ForegroundColor = ConsoleColor.DarkGray;
Console.Write($"Frame t={t:F3} | X={posX,3} | Y={posY,2} |FPS=24");
// Dibujar el personaje en la posición calculada
Console.SetCursorPosition(posX, posY);
Console.ForegroundColor = ConsoleColor.Cyan;
Console.Write(PERSONAJE);
Console.ResetColor();
// ─── CONTROL DE TIEMPO
// Esperar el tiempo restante del frame para mantener 24 FPS
Thread.Sleep(intervaloMs);
}
// Mensaje final al terminar la animación
Console.Clear();
Console.SetCursorPosition(ANCHO_CONSOLA / 2 - 10,
ALTO_CONSOLA / 2);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("¡Animación completada! ✓");
Console.ResetColor();
Console.ReadKey();
    }

}

