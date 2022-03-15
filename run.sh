#!/bin/sh
pip install virtualenv
python3 -m virtualenv pem-ml-env
source pem-ml-env/bin/activate
pip3 install mlagents
/home/r/riessf/.local/bin/mlagents-learn ./config/Volleyball.yaml --env=./competitive-env/RobotVolleyBallEnvironment.x86_64 --run-id=competitive-six-joins-run-1