import React from 'react';
import Graph from './graph';

class AppGraphContent extends React.Component {

  
    constructor(props) {
      
        super(props);
        this.canvasRef = React.createRef();
    }

    // ******************* COMPONENT LIFECYCLE ******************* //
    componentDidMount() {
        // Get canvas, pass to custom class
        const canvas = this.canvasRef.current;

        this.graph = new Graph(canvas);

        // Init any event listeners
        window.addEventListener('wheel', this.mouseWheel);
        window.addEventListener('resize', this.handleResize);
        window.addEventListener('mousedown', this.onPointerDown);
        window.addEventListener('touchstart', (e) => this.handleTouch(e, this.onPointerDown))
        window.addEventListener('mouseup', this.onPointerUp)
        window.addEventListener('touchend',  (e) => this.handleTouch(e, this.onPointerUp))
        window.addEventListener('mousemove', this.onPointerMove)
        window.addEventListener('touchmove', (e) => this.handleTouch(e, this.onPointerMove))
    }

    componentDidUpdate(prevProps, prevState) {
        // Pass updated props to 
        const newValue = this.props.whateverProperty;
        this.Graph.updateValue(newValue);
    }


    // ******************* EVENT LISTENERS ******************* //
    mouseWheel = (event) => {
        this.graph.onMouseWheel();
    }

    handleResize = () => {
        this.graph.onWindowResize(window.innerWidth, window.innerHeight);
    };

    onPointerDown = () => {
        this.graph.onPointerDown();
    }

    handleTouch = (e) => {
        this.graph.handleTouch(e, this.onPointerDown);
    }

    handleTouch = (e) => {
      this.graph.handleTouch(e, this.onPointerUp);
  }

  handleTouch = (e) => {
    this.graph.handleTouch(e, this.onPointerMove);
}

    onPointerUp = () => {
      this.graph.onPointerUp();
    }

    onPointerMove = () => {
      this.graph.onPointerMove();
    }


    render() {
        return (
            <div className="canvasContainer">
                <canvas ref={this.canvasRef} />
            </div>
        );
    }
}

export default function AppGraph() {
  return <AppGraphContent />;
}
