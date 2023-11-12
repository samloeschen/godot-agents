shader_type canvas_item;
render_mode blend_premul_alpha;
uniform sampler2D srcA : source_color, filter_nearest;




// Precision-adjusted variations of https://www.shadertoy.com/view/4djSRW
float hash(float p) { p = fract(p * 0.011); p *= p + 7.5; p *= p + p; return fract(p); }
float hash2(vec2 p) {vec3 p3 = fract(vec3(p.xyx) * 0.13); p3 += dot(p3, p3.yzx + 3.333); return fract((p3.x + p3.y) * p3.z); }

float noise(float x) {
    float i = floor(x);
    float f = fract(x);
    float u = f * f * (3.0 - 2.0 * f);
    return mix(hash(i), hash(i + 1.0), u);
}


float noise2(vec2 x) {
    vec2 i = floor(x);
    vec2 f = fract(x);

	// Four corners in 2D of a tile
	float a = hash2(i);
    float b = hash2(i + vec2(1.0, 0.0));
    float c = hash2(i + vec2(0.0, 1.0));
    float d = hash2(i + vec2(1.0, 1.0));

    // Simple 2D lerp using smoothstep envelope between the values.
	// return vec3(mix(mix(a, b, smoothstep(0.0, 1.0, f.x)),
	//			mix(c, d, smoothstep(0.0, 1.0, f.x)),
	//			smoothstep(0.0, 1.0, f.y)));

	// Same code, with the clamps in smoothstep and common subexpressions
	// optimized away.
    vec2 u = f * f * (3.0 - 2.0 * f);
	return mix(a, b, u.x) + (c - a) * u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
}


vec2 circhash( float n )
{
    float sn = sin(n);
    return fract(vec2(sn,sn*42125.13));
}

float circleNoise( vec2 uv )
{
    float uv_y = floor(uv.y);
    uv.x += uv_y*.31;
    vec2 f = fract(uv);
    vec2 h = circhash(floor(uv.x)*uv_y);
    float m = (length(f-.25-(h.x*.5)));
    float r = h.y*.25;
    return m = smoothstep(r-.10*r,r,m);
}


void fragment() {
	vec4 c = textureLod(srcA, UV, 0.0);

	
	vec2 p = UV + noise2(UV * 1.5) * 0.2;
	float n = circleNoise(p * 10.0 + vec2(noise2(UV * 2.0), TIME * 1.0));
	
	
	
	if (n < 1.0 && c.a < 1.0) {
		c.a -= 0.15;
	}

	c *= 0.95;
	if (c.a < 0.01) {
		c = vec4(0);
	}
	COLOR.rgba = c;//vec4(vec3(n), 1.0);
}