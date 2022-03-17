#!/bin/bash
pip install virtualenv
python3 -m virtualenv pem-ml-env
source pem-ml-env/bin/activate
pip3 install mlagents
mlagents-learn ./config/Volleyball.yaml --env=./env/UnityEnvironment.x86_64 --run-id=run-1