shader_type canvas_item;
uniform sampler2D src : source_color, filter_nearest;
uniform float fun_alpha = 1;
void fragment() {
    COLOR.rgba = textureLod(src, UV, 0.0) * fun_alpha;
}
    