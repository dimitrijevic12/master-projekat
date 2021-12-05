import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getTransportationsForOwner } from "../../actions/actionsTransportations";
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
    await this.props.getTransportationsForOwner();
    await this.getAllImages(this.props.transportationsForOwner);
  }
  render() {
    if (this.props.transportationsForOwner === undefined) {
      return null;
    }

    debugger;
    const transportationsForOwner = this.props.transportationsForOwner;

    if (this.props.itemsImages === undefined) {
      return null;
    }

    if (
      this.props.itemsImages.length !==
      this.props.transportationsForOwner.length
    ) {
      return null;
    }

    debugger;
    const TransportationsList = () => {
      debugger;
      return transportationsForOwner.map((post, i) =>
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
        <div className="mt-5 pb-5">
          <button
            onClick={() => {
              this.createItem();
            }}
            className="btn btn-primary btn-block"
          >
            Create new transportation
          </button>
        </div>
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>
              Your Transportations
            </GridControlBarItem>
          </GridControlBar>
          <TransportationsList />
        </Grid>
      </div>
    );
  }

  createItem() {
    window.location = "/create-transportation";
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
  transportationsForOwner: state.transportationsForOwner,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getTransportationsForOwner,
    getImagesForItems,
  })
)(Transportations);
