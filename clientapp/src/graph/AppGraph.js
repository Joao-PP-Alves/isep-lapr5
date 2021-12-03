import React, { createRef, useEffect } from 'react';
import Graph from './graph'


export default function AppGraph() {
  var scene;
  var camera;
  var render;
  var divRef = createRef();
  useEffect(() => new Graph(), divRef=[{scene,camera,render}]);
  return (
      <div ref={divRef} />
  );
}
