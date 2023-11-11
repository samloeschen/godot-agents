shader_type canvas_item;
uniform sampler2D src : source_color, filter_nearest;
void fragment() {
    COLOR.rgba = textureLod(src, UV, 0.0);
}
    