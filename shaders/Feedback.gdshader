shader_type canvas_item;
render_mode blend_premul_alpha;
uniform sampler2D srcA : source_color, filter_nearest;
void fragment() {
	vec4 c = textureLod(srcA, UV, 0.0);
	c *= 0.95;
	if (dot(c, c) < 0.2) {
		c = vec4(0.0);
	}
	COLOR.rgba = c;
}