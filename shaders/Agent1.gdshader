shader_type canvas_item;
uniform vec4 color : source_color;

#include "SDF2D.gdshaderinc"
void fragment() {
	float circ = smoothstep(0.3, 0, sdCircle(UV - 0.5, 0.15));
	COLOR.rgba = COLOR * vec4(1.0, 1.0, 1.0, circ);
}
