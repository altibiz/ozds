# Users

This portion of the documentation is meant for the end users of the OZDS web
application. This documentation outlines all functionality and features of the
OZDS web application divided for certain roles of users.

The [admin](../../user/admin/index.md) portion describes all functionality and
features for administrators of the application. These are not considered end
users and are usually developers.

## Login

On the login page, users start their session. After logging in, the user is
redirected to a page personalized to their needs based on their user type,
privileges, and the locations and network users they are responsible for.

![Login](../../assets/login.png) _/login_

## Meter

When one of the measurement locations is clicked, detailed data about that
measurement location becomes visible. The initial and final readings at that
measurement location per month are displayed, along with the total consumption
and maximum power during that month. A graph on the right side shows
measurements for the last quarter hour, half hour, hour, and six hours, and it
can display values for voltage, current, active power, reactive power, and
apparent power. A gauge on the left side shows the current power and compares it
to the maximum power from the previous month.

![Meter](../../assets/meter.png) _/app/meter_
