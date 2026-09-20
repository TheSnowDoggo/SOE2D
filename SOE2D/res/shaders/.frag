#version 330 core

out vec4 _color;

uniform vec4 modulate;

void main() {
    _color = modulate * vec4(1.0, 1.0, 1.0, 1.0);
}