import * as THREE from 'three';


/**   parameters = {
   color: Color,
   user: User,
   parent : T,
   x: Number,
   y: Number,
  }
 */

export default class Node {
    constructor(parameters,scene) {
        for (const [key, value] of Object.entries(parameters)) {
            Object.defineProperty(this, key, { value: value, writable: true, configurable: true, enumerable: true });
        }

        // Create the ball (a circle)
        const geometry = new THREE.CircleGeometry(this.radius, 16);
        const material = new THREE.MeshBasicMaterial({ color: this.color });
        this.object = new THREE.Mesh(geometry, material);

        this.initialize(scene);
    }

    initialize() {
        this.center = new THREE.Vector3(this.x, this.y, 0.0);
        this.object.position.set(this.center.x, this.center.y, this.center.z);
        scene.add(this.object);
    }
}