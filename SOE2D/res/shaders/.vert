#version 330 core

layout (location = 0) in vec2 _pos;

void main() {
    gl_Position = vec4(_pos, 0, 1);
}