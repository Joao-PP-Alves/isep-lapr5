import * as THREE from 'three';
import Orientation from './orientation';

export const camera_const = {
    view: 'top', // Fixed view: 'fixed'; first-person view: 'first-person'; third-person view: 'third-person'; top view: 'top'; mini-map: 'mini-map'
    multipleViewsViewport: new THREE.Vector4(0.0, 0.0, 1.0, 1.0), // Viewport position and size: % of window width and window height; MUST BE REDEFINED when creating an instance of ThumbRaiser() so that each view is assigned a different viewport
    target: new THREE.Vector3(0.0, 0.0, 0.0), // Target position
    initialOrientation: new Orientation(135.0, -45.0), // Horizontal and vertical orientation and associated limits (expressed in degrees)
    orientationMin: new Orientation(-180.0, -90.0),
    orientationMax: new Orientation(180.0, 0.0),
    initialDistance: 8.0, // Distance to the target and associated limits
    distanceMin: 4.0,
    distanceMax: 16.0,
    initialZoom: 1.0, // Zoom factor and associated limits
    zoomMin: 0.5,
    zoomMax: 2.0,
    initialFov: 45.0, // Field-of-view (expressed in degrees)
    near: 0.01, // Front clipping plane
    far: 100.0 // Back clipping plane
}
