shader_type canvas_item;
render_mode blend_premul_alpha;
uniform sampler2D src : source_color, filter_nearest;
void fragment() {
    COLOR.rgba = textureLod(src, UV, 0.0);
}
    