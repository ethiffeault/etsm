
# Publish 

- bump version setup.py
- py setup.py sdist bdist_wheel
- py -m twine upload --verbose dist/* 
