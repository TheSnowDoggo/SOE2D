#version 330 core

layout (location = 0) in vec2 _pos;

uniform mat4 mp;

void main() {
    gl_Position = mp * vec4(_pos, 0, 1);
}