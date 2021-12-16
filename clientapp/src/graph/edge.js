import * as THREE from 'three';
import Node from './node.js';

/**parameters = {
    parent : Node,
    friend : Node,
    ligacao : Number,
    relacao : Number
}*/

export default class Edge{
    constructor(parameters,scene){
        for (const [key, value] of Object.entries(parameters)) {
            Object.defineProperty(this, key, { value: value, writable: true, configurable: true, enumerable: true });
        }

        const material = new THREE.MeshBasicMaterial( {color: 0xffff00} );
        let vectorX = new THREE.Vector3( this.parent.x, this.parent.y, 0);
        let vectorY = new THREE.Vector3( this.friend.x, this.friend.y, 0);
        let mesh = this.cylinderMesh(vectorX,vectorY,this.ligacao,material);
        scene.add(mesh);

    }

    returnForcaLigacao(){
        return this.parameters.ligacao;
    }

    cylinderMesh(pointX, pointY,size, material) {
        var direction = new THREE.Vector3().subVectors(pointY, pointX);
        var orientation = new THREE.Matrix4();
        orientation.lookAt(pointX, pointY, new THREE.Object3D().up);
        orientation.multiply(new THREE.Matrix4().set(1, 0, 0, 0,
            0, 0, 1, 0,
            0, -1, 0, 0,
            0, 0, 0, 1));
        var edgeGeometry = new THREE.CylinderGeometry( size*0.15,size*0.15, direction.length(), 16, 16);
        var edge = new THREE.Mesh(edgeGeometry, material);
        edge.applyMatrix(orientation);
        edge.position.x = (pointY.x + pointX.x) / 2;
        edge.position.y = (pointY.y + pointX.y) / 2;
        edge.position.z = (pointY.z + pointX.z) / 2;
        return edge;
    }
}