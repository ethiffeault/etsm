
# Publish 

- bump version setup.py
- py setup.py sdist bdist_wheel
- install it the current machine: py -m pip  install -e .
- py -m twine upload --verbose dist/* 