using Godot;
static class CustomShaders {
    // canvas item shaders
    const string CANVAS_BLIT_CODE = @"
		shader_type canvas_item;
		uniform sampler2D src : source_color, filter_nearest;
		void fragment() {
			COLOR.rgba = textureLod(src, UV, 0.0);
		}
    ";
    public static readonly Shader CANVAS_BLIT = new() {
        Code = CANVAS_BLIT_CODE,
    };


    const string UV_TEST_CODE = @"
		shader_type canvas_item;
		uniform sampler2D src : source_color, filter_nearest;
		void fragment() {
			COLOR.rgba = vec4(UV, 1.0, 1.0);
		}
    ";
    public static readonly Shader UV_TEST = new() {
        Code = UV_TEST_CODE,
    };
}
    