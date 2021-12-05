import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getAccommodationsForOwner } from "../../actions/actionsAccommodation";
import { getImagesForItems } from "../../actions/actionsItems";

class OwnerAccommodations extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
    city: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getAccommodationsForOwner();
    await this.getAllImages(this.props.accommodationsForOwner);
  }
  render() {
    if (this.props.accommodationsForOwner === undefined) {
      return null;
    }

    debugger;
    const accommodationsForOwner = this.props.accommodationsForOwner;

    if (this.props.itemsImages === undefined) {
      return null;
    }

    if (
      this.props.itemsImages.length !== this.props.accommodationsForOwner.length
    ) {
      return null;
    }

    debugger;
    const AccommodationsList = () => {
      debugger;
      return accommodationsForOwner.map((post, i) =>
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
            Create new accommodation
          </button>
        </div>
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Accommodations</GridControlBarItem>
          </GridControlBar>
          <AccommodationsList />
        </Grid>
      </div>
    );
  }

  createItem() {
    window.location = "/create-accommodation";
  }

  view(f) {
    localStorage.setItem("accommodation-id", f.id);
    window.location = "/accommodation/" + f.id;
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
  accommodationsForOwner: state.accommodationsForOwner,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getAccommodationsForOwner,
    getImagesForItems,
  })
)(OwnerAccommodations);
