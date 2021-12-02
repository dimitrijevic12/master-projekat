import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getImagesForItems } from "../../actions/actionsItems";
import { getCourses } from "../../actions/actionsCourse";

class Courses extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getCourses();
    await this.getAllImages(this.props.courses);
  }
  render() {
    if (this.props.courses === undefined) {
      return null;
    }

    debugger;
    const courses = this.props.courses;

    if (this.props.itemsImages === undefined) {
      return null;
    }
    debugger;
    const CoursesList = () => {
      debugger;
      return courses.map((post, i) =>
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
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Courses</GridControlBarItem>
          </GridControlBar>
          <CoursesList />
        </Grid>
      </div>
    );
  }

  view(f) {
    localStorage.setItem("course-id", f.id);
    window.location = "/course/" + f.id;
  }

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
  courses: state.courses,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getCourses,
    getImagesForItems,
  })
)(Courses);
