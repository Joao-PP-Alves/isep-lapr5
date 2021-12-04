import React, { createRef, useEffect } from 'react';
import Graph from './graph'
import camera_zoom from './camera_zoom'


export default function AppGraph() {
  var divRef = createRef();
  useEffect(() => new Graph(divRef), [divRef ]);
  return (
      <div ref={divRef} />
  ); 
}
