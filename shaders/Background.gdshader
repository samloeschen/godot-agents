shader_type canvas_item;
uniform sampler2D src : source_color, filter_nearest;
void fragment() {
	COLOR.rgba = vec4(UV, 1.0, 1.0);
}