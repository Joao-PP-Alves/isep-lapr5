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

        let points = [];
        points.push( new THREE.Vector3( this.parent.x, this.parent.y, 0) );
        points.push( new THREE.Vector3( this.friend.x, this.friend.y, 0 ) );

        const material = new THREE.LineBasicMaterial( { color: 0x777777, linewidth : this.ligacao} );
        
        const geometry = new THREE.BufferGeometry().setFromPoints(points);
        const line = new THREE.Line( geometry, material );
        
        scene.add(line);
    }

    returnForcaLigacao(){
        return this.parameters.ligacao;
    }
}