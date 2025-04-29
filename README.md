# Pixelation Post-Processing Shader for Unity

This Unity shader applies a retro-style **pixelation effect** to your camera output using a custom post-processing shader. It simulates a low-resolution look by sampling the center of screen-space blocks and rendering a pixelated version of your scene.



## Demo Preview

![Pixel Shader](https://github.com/ilialek/Resources/blob/main/Pixel%20shader%20demonstration.png)



## What It Does

This shader performs full-screen post-processing that:
- Divides the screen into a grid of rectangular blocks.
- Samples the center pixel of each block from the original scene.
- Replaces all pixels in that block with the sampled color.
This creates a pixelated image, ideal for retro visuals or stylized effects.
