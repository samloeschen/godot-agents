shader_type canvas_item;

uniform sampler2D src : source_color, filter_nearest;
uniform sampler2D pal : source_color, filter_nearest;


#define iTime TIME * 0.5

void fragment() {
	
    // background	 
    vec3 color = vec3(1.0);

    // bubbles	
    for( int i=0; i<100; i++ )
    {
          // bubble seeds
      float pha =      sin(float(i)*546.13+1.0)*0.5 + 0.5;
      float siz = pow( sin(float(i)*651.74+5.0)*0.5 + 0.5, 4.0 );
      float pox =      sin(float(i)*321.55+4.1);

          // buble size, position and color
      float rad = 0.1 + 0.5*siz+sin(iTime/6.+pha*5000.0+siz)/20.0;
      vec2  pos = vec2( pox+sin(iTime/10.+pha+siz), -1.0-rad + (2.0+2.0*rad)
              *mod(pha+0.1*(iTime/5.0)*(0.2+0.8*siz),1.0));
      float dis = length( UV - pos );

        // render
      float f = length(UV-pos)/rad;
      f = sqrt(clamp(1.0+(sin((iTime/7.0)+pha*500.0+siz)*0.5)-f*f,0.0,1.0));

		float stepsize = 72.0 / 16.0;
		int palIdx = (i % 13) + 1;
		vec3 palette = 1.0 - textureLod(pal, vec2(float(palIdx) * stepsize / 72.0, 0.5), 0.0).rgb;
		
		float fuzz = i % 2 == 0 ? 0.9 : 0.01;
		color -= palette * (1.0 - smoothstep(rad * fuzz, rad, dis )) * f * 0.35 * step(dis, rad * 0.8);
    }
	
	
	COLOR.rgba =  vec4((color * 1.2 - 0.2), 1.0);
	
	
}