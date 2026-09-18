#version 330 core

layout (location = 0) in vec2 _pos;

uniform mat4 mv;

void main() {
    gl_Position = vec4(_pos, 0, 1);
}