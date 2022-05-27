
# Publish

- bump version in etsm.gemspec
- gem build etsm.gemspec
- install locally to test: gem install ./etsm-x.y.z.gem
- create credential if needed: 
  - open bash shell in vscode: curl -u ethiffeault https://rubygems.org/api/v1/api_key.yaml > ~/.gem/credentials; chmod 0600 ~/.gem/credentials
- gem push etsm-x.y.z.gem
- goto: https://rubygems.org/search?query=etsm