import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getTransportations } from "../../actions/actionsTransportations";
import { getImagesForItems } from "../../actions/actionsItems";

class Transportations extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
    startDestination: "",
    finalDestination: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getTransportations();
    await this.getAllImages(this.props.transportations);
  }
  render() {
    if (this.props.transportations === undefined) {
      return null;
    }

    debugger;
    const transportations = this.props.transportations;

    if (this.props.itemsImages === undefined) {
      return null;
    }

    if (this.props.itemsImages.length !== this.props.transportations.length) {
      return null;
    }

    debugger;
    const TransportationsList = () => {
      debugger;
      return transportations.map((post, i) =>
        this.props.itemsImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={
              "data:image/jpg;base64," + this.props.itemsImages[i].fileContents
            }
            onClick={() => this.view(post)}
          />
        ) : (
          <video
            controls
            onClick={() => this.view(post)}
            style={{ width: 367, height: 370 }}
            className="mb-3"
          >
            <source
              src={
                "data:video/mp4;base64," +
                this.props.itemsImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    };

    return (
      <div>
        <div className="mt-5">
          <div className="d-inline-flex w-45">
            <div class="form-group w-100 pr-5">
              <label for="tag">Start Destination:</label>
              <input
                type="text"
                name="startDestination"
                value={this.state.startDestination}
                onChange={this.handleChange}
                class="form-control"
                id="startDestination"
                placeholder="Enter start destination"
              />
            </div>
          </div>
          <span style={{ width: 30, display: "inline-block" }}></span>
          <div className="d-inline-flex w-45">
            <div class="form-group w-100 pr-5">
              <label for="tag">Final Destination:</label>
              <input
                type="text"
                name="finalDestination"
                value={this.state.finalDestination}
                onChange={this.handleChange}
                class="form-control"
                id="finalDestination"
                placeholder="Enter final destination"
              />
            </div>
          </div>
          <div className="d-inline-flex w-50">
            <div class="form-group w-40 pl-4">
              <br />
              <span style={{ width: 30, display: "inline-block" }}></span>
              <img
                onClick={() => {
                  this.search();
                }}
                src="/images/analytics.png"
              />
            </div>
          </div>
        </div>
        <br />
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Transportations</GridControlBarItem>
          </GridControlBar>
          <TransportationsList />
        </Grid>
      </div>
    );
  }

  async search() {
    debugger;
    if (
      this.state.startDestination === "" &&
      this.state.finalDestination === ""
    ) {
      await this.props.getTransportations();
    } else if (
      this.state.startDestination !== "" &&
      this.state.finalDestination === ""
    ) {
      await this.props.getTransportations(this.state.startDestination, null);
    } else if (
      this.state.startDestination === "" &&
      this.state.finalDestination !== ""
    ) {
      await this.props.getTransportations(null, this.state.finalDestination);
    } else {
      await this.props.getTransportations(
        this.state.startDestination,
        this.state.finalDestination
      );
    }

    await this.getAllImages(this.props.transportations);
  }

  view(f) {
    localStorage.setItem("transportation-id", f.id);
    window.location = "/transportation/" + f.id;
  }

  handleChange = (event) => {
    const { name, value, type, checked } = event.target;
    debugger;
    this.setState({
      [name]: value,
    });
  };

  getAllImages = async (items) => {
    debugger;
    const paths = [];
    for (var i = 0; i < items.length; i++) {
      if (items[i].imagePath === undefined) {
        paths.push(items[i].imagePaths[0]);
      } else {
        paths.push(items[i].imagePath);
      }
    }
    await this.props.getImagesForItems(paths);
  };
}

const mapStateToProps = (state) => ({
  transportations: state.transportations,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getTransportations,
    getImagesForItems,
  })
)(Transportations);
